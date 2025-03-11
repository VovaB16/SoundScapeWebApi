using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundScape.Data;
using SoundScape.DTOs;
using SoundScape.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoundScape.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public FavoritesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost("add/{trackId}")]
        public async Task<IActionResult> AddFavoriteTrack(int trackId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }

            var user = await _dbContext.Users.Include(u => u.FavoriteTracks).FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var track = await _dbContext.MusicTracks.FindAsync(trackId);
            if (track == null)
            {
                return NotFound("Track not found.");
            }

            user.FavoriteTracks.Add(track);
            await _dbContext.SaveChangesAsync();

            return Ok("Track added to favorites.");
        }

        [Authorize]
        [HttpDelete("remove/{trackId}")]
        public async Task<IActionResult> RemoveFavoriteTrack(int trackId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }

            var user = await _dbContext.Users.Include(u => u.FavoriteTracks).FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var track = user.FavoriteTracks.FirstOrDefault(t => t.Id == trackId);
            if (track == null)
            {
                return NotFound("Track not found in favorites.");
            }

            user.FavoriteTracks.Remove(track);
            await _dbContext.SaveChangesAsync();

            return Ok("Track removed from favorites.");
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetFavoriteTracks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }

            var user = await _dbContext.Users.Include(u => u.FavoriteTracks).FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var favoriteTracks = user.FavoriteTracks.Select(t => new TrackDto
            {
                Id = t.Id,
                Title = t.Title,
                Artist = t.Artist,
                Album = t.Album,
                Genre = t.Genre,
                Duration = t.Duration,
                FilePath = t.FilePath,
                UploadDate = t.UploadDate,
                ImageUrl = t.ImageUrl
            }).ToList();

            return Ok(favoriteTracks);
        }
    }
}

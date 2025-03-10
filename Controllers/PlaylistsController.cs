using Microsoft.AspNetCore.Mvc;
using SoundScape.Models;
using SoundScape.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SoundScape.DTOs;
using System.Security.Claims;

namespace SoundScape.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string DefaultImageUrl = "/images/playlist-default-icon.svg";

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlaylist([FromForm] CreatePlaylistDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }

            var playlist = new Playlist
            {
                Name = model.Name,
                Description = model.Description,
                OwnerId = int.Parse(userId),
                CreationDate = DateTime.UtcNow
            };
            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                playlist.ImageUrl = $"/images/{uniqueFileName}";
            }

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Playlist created successfully", playlistId = playlist.Id, imageUrl = playlist.ImageUrl });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{playlistId}/tracks")]
        public async Task<IActionResult> AddTrackToPlaylist(int playlistId, [FromBody] TrackDto trackDto)
        {
            var playlist = await _context.Playlists
                .Include(p => p.PlaylistTracks)
                .FirstOrDefaultAsync(p => p.Id == playlistId);
            if (playlist == null)
            {
                return NotFound();
            }

            var track = new Track
            {
                Title = trackDto.Title,
                Artist = trackDto.Artist,
                Album = trackDto.Album,
                Genre = trackDto.Genre,
                Duration = trackDto.Duration,
                FilePath = trackDto.FilePath,
                UploadDate = trackDto.UploadDate,
                ImageUrl = trackDto.ImageUrl
            };

            playlist.PlaylistTracks.Add(new PlaylistTrack { PlaylistId = playlistId, Track = track });
            await _context.SaveChangesAsync();
            return Ok(playlist);
        }

        [HttpPost("{playlistId}/tracks/{trackId}")]
        public async Task<IActionResult> AddTrackToPlaylist(int playlistId, int trackId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.PlaylistTracks)
                .FirstOrDefaultAsync(p => p.Id == playlistId);
            if (playlist == null)
            {
                return NotFound("Playlist not found.");
            }

            var track = await _context.MusicTracks.FindAsync(trackId);
            if (track == null)
            {
                return NotFound("Track not found.");
            }

            var existingPlaylistTrack = await _context.PlaylistTracks
                .FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
            if (existingPlaylistTrack != null)
            {
                return BadRequest("The track is already in the playlist.");
            }

            playlist.PlaylistTracks.Add(new PlaylistTrack { PlaylistId = playlistId, TrackId = trackId });
            await _context.SaveChangesAsync();
            return Ok(playlist);
        }



        [HttpDelete("{playlistId}/tracks/{trackId}")]
        public async Task<IActionResult> RemoveTrackFromPlaylist(int playlistId, int trackId)
        {
            var playlistTrack = await _context.PlaylistTracks
                .FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
            if (playlistTrack == null)
            {
                return NotFound();
            }

            _context.PlaylistTracks.Remove(playlistTrack);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPlaylist(int id, [FromForm] UpdatePlaylistDto updatedPlaylistDto)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            playlist.Name = updatedPlaylistDto.Name;
            playlist.Description = updatedPlaylistDto.Description;

            if (updatedPlaylistDto.Image != null)
            {

                var imagePath = Path.Combine("wwwroot/images/playlists", updatedPlaylistDto.Image.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await updatedPlaylistDto.Image.CopyToAsync(stream);
                }
                playlist.ImageUrl = $"/images/playlists/{updatedPlaylistDto.Image.FileName}";
            }

            await _context.SaveChangesAsync();
            return Ok(playlist);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists
                .Include(p => p.PlaylistTracks)
                .ThenInclude(pt => pt.Track)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }


        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserPlaylists()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var playlists = await _context.Playlists
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            if (playlists == null || !playlists.Any())
            {
                return NotFound("No playlists found for the authenticated user.");
            }

            return Ok(playlists);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _context.Playlists
                .Include(p => p.PlaylistTracks)
                .ThenInclude(pt => pt.Track)
                .ToListAsync();

            return Ok(playlists);
        }

    }


}

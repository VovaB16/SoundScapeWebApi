using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundScape.Data;
using SoundScape.DTOs;
using SoundScape.Models;

namespace SoundScape.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularArtists()
        {
            var artists = await _context.ArtistPopularities
                .OrderByDescending(a => a.Listens + a.Likes)
                .Select(a => new {
                    a.Artist.Id,
                    a.Artist.Name,
                    a.Artist.ImageUrl,
                    a.Listens,
                    a.Likes
                })
                .Take(10)
                .ToListAsync();

            return Ok(artists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] CreateArtistDto createArtistDto)
        {
            if (createArtistDto == null)
            {
                return BadRequest("Artist data is null.");
            }

            var artist = new Artist
            {
                Name = createArtistDto.Name,
                ImageUrl = createArtistDto.ImageUrl
            };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPopularArtists), new { id = artist.Id }, artist);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _context.Artists
                .Select(a => new {
                    a.Id,
                    a.Name,
                    a.ImageUrl
                })
                .ToListAsync();

            return Ok(artists);
        }
    }
}

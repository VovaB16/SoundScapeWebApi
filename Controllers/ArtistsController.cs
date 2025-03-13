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

            if (_context.Artists.Any(a => a.Name == createArtistDto.Name))
            {
                return Conflict("An artist with the same name already exists.");
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _context.Artists
                .Where(a => a.Id == id)
                .Select(a => new {
                    a.Id,
                    a.Name,
                    a.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpGet("name/{title}")]
        public async Task<IActionResult> GetArtistByTitle(string title)
        {
            var artist = await _context.Artists
                .Where(a => a.Name == title)
                .Select(a => new {
                    a.Id,
                    a.Name,
                    a.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }
        [HttpGet("{artistId}/singles")]
        public async Task<IActionResult> GetSinglesByArtist(int artistId)
        {
            var singles = await _context.Singles
                .Where(s => s.ArtistId == artistId)
                .ToListAsync();

            if (singles == null || !singles.Any())
            {
                return NotFound();
            }

            return Ok(singles);
        }

        [HttpPost("{artistId}/singles")]
        public async Task<IActionResult> CreateSingle(int artistId, [FromBody] CreateSingleDto createSingleDto)
        {
            if (createSingleDto == null)
            {
                return BadRequest("Single data is null.");
            }

            var single = new Single
            {
                Title = createSingleDto.Title,
                ReleaseDate = createSingleDto.ReleaseDate,
                ArtistId = artistId,
                Genre = createSingleDto.Genre,
                Duration = createSingleDto.Duration,
                FilePath = createSingleDto.FilePath,
                ImageUrl = createSingleDto.ImageUrl
            };

            _context.Singles.Add(single);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSinglesByArtist), new { artistId = artistId, id = single.Id }, single);
        }

        [HttpPut("singles/{id}")]
        public async Task<IActionResult> UpdateSingle(int id, [FromBody] UpdateSingleDto updateSingleDto)
        {
            if (updateSingleDto == null)
            {
                return BadRequest("Single data is null.");
            }

            var single = await _context.Singles.FindAsync(id);
            if (single == null)
            {
                return NotFound();
            }

            single.Title = updateSingleDto.Title;
            single.ReleaseDate = updateSingleDto.ReleaseDate;
            single.Genre = updateSingleDto.Genre;
            single.Duration = updateSingleDto.Duration;
            single.FilePath = updateSingleDto.FilePath;
            single.ImageUrl = updateSingleDto.ImageUrl;

            _context.Singles.Update(single);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("singles/{id}")]
        public async Task<IActionResult> DeleteSingle(int id)
        {
            var single = await _context.Singles.FindAsync(id);
            if (single == null)
            {
                return NotFound();
            }

            _context.Singles.Remove(single);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{artistId}/subscribe")]
        [Authorize]
        public async Task<IActionResult> SubscribeToArtist(int artistId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var subscription = new Subscription
            {
                UserId = userId,
                ArtistId = artistId
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Subscribed to artist successfully." });
        }

        [HttpDelete("{artistId}/unsubscribe")]
        [Authorize]
        public async Task<IActionResult> UnsubscribeFromArtist(int artistId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.ArtistId == artistId);

            if (subscription == null)
            {
                return NotFound(new { message = "Subscription not found." });
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Unsubscribed from artist successfully." });
        }

        [HttpGet("subscriptions")]
        [Authorize]
        public async Task<IActionResult> GetUserSubscriptions()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var subscriptions = await _context.Subscriptions
                .Where(s => s.UserId == userId)
                .Select(s => new {
                    s.Artist.Id,
                    s.Artist.Name,
                    s.Artist.ImageUrl
                })
                .ToListAsync();

            return Ok(subscriptions);
        }
        [HttpGet("{artistId}/isSubscribed")]
        [Authorize]
        public async Task<IActionResult> IsUserSubscribed(int artistId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isSubscribed = await _context.Subscriptions
                .AnyAsync(s => s.UserId == userId && s.ArtistId == artistId);

            return Ok(new { isSubscribed });
        }
    }
}

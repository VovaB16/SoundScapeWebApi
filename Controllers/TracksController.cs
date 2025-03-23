using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundScape.Data;
using SoundScape.Models;
using System.Diagnostics;
using System.Linq;

namespace SoundScape.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TracksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await _dbContext.MusicTracks.ToListAsync();
            return Ok(tracks);
        }

        [HttpGet]
        public IActionResult GetTracks()
        {
            var tracks = _dbContext.MusicTracks.ToList();
            return Ok(tracks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTrack(int id)
        {
            var track = _dbContext.MusicTracks.FirstOrDefault(t => t.Id == id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        [HttpGet("search")]
        public IActionResult SearchTracks(string title = null, string artist = null, string genre = null)
        {
            var query = _dbContext.MusicTracks.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(t => t.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(artist))
            {
                query = query.Where(t => t.Artist.Contains(artist));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(t => t.Genre.Contains(genre));
            }

            var tracks = query.ToList();
            return Ok(tracks);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTrack(int id)
        {
            var track = _dbContext.MusicTracks.FirstOrDefault(t => t.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            _dbContext.MusicTracks.Remove(track);
            _dbContext.SaveChanges();
            return NoContent();
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("add-track")]
        public async Task<IActionResult> AddTrack([FromForm] string title, [FromForm] IFormFile trackFile, [FromForm] IFormFile imageFile)
        {
            if (trackFile == null || imageFile == null)
            {
                return BadRequest("Track file and image file are required.");
            }

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var trackFileName = $"{Guid.NewGuid()}{Path.GetExtension(trackFile.FileName)}";
            var imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

            var trackFilePath = Path.Combine(webRootPath, "tracks", trackFileName);
            var imageFilePath = Path.Combine(webRootPath, "images", imageFileName);

            await using (var trackStream = new FileStream(trackFilePath, FileMode.Create))
            {
                await trackFile.CopyToAsync(trackStream);
            }

            await using (var imageStream = new FileStream(imageFilePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(imageStream);
            }

            var track = new Track
            {
                Title = title,
                FilePath = $"/tracks/{trackFileName}",
                ImageUrl = $"/images/{imageFileName}",
                UploadDate = DateTime.UtcNow,
                Artist = "Admin",
                Album = "Admin",
                Genre = "Admin",
                Duration = "2"
            };

            _dbContext.MusicTracks.Add(track);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Track added successfully.", track });
        }


    }
}

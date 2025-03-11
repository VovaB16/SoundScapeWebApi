using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundScape.Data;
using SoundScape.Models;
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
    }
}

using Microsoft.AspNetCore.Mvc;
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
    }
}

using Microsoft.AspNetCore.Mvc;
using SoundScape.Data;
using SoundScape.Models;
using System.Linq;

namespace SoundScape.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAlbums()
        {
            var albums = _dbContext.Albums.ToList();
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id)
        {
            var album = _dbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        [HttpPost]
        public IActionResult CreateAlbum([FromBody] Album newAlbum)
        {
            if (newAlbum == null)
            {
                return BadRequest();
            }
            newAlbum.ReleaseDate = newAlbum.ReleaseDate.ToUniversalTime();

            _dbContext.Albums.Add(newAlbum);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAlbum), new { id = newAlbum.Id }, newAlbum);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAlbum(int id, [FromBody] Album updatedAlbum)
        {
            if (updatedAlbum == null || updatedAlbum.Id != id)
            {
                return BadRequest();
            }

            var album = _dbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            album.Title = updatedAlbum.Title;
            album.ReleaseDate = updatedAlbum.ReleaseDate;
            album.ArtistId = updatedAlbum.ArtistId;

            _dbContext.Albums.Update(album);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            var album = _dbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            _dbContext.Albums.Remove(album);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("artist/{artistId}")]
        public IActionResult GetAlbumsByArtist(int artistId)
        {
            var albums = _dbContext.Albums.Where(a => a.ArtistId == artistId).ToList();
            if (albums == null || !albums.Any())
            {
                return NotFound();
            }
            return Ok(albums);
        }
    }
}

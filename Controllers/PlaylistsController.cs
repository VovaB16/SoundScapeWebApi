using Microsoft.AspNetCore.Mvc;
using SoundScape.Models;
using SoundScape.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SoundScape.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreatePlaylist([FromBody] Playlist playlist)
        {
            if (playlist == null)
            {
                return BadRequest("Playlist is null.");
            }

            playlist.CreationDate = DateTime.SpecifyKind(playlist.CreationDate, DateTimeKind.Utc);

            _context.Playlists.Add(playlist);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPlaylistById), new { id = playlist.Id }, playlist);
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
        public async Task<IActionResult> AddTrackToPlaylist(int playlistId, [FromBody] Track track)
        {
            var playlist = await _context.Playlists
                .Include(p => p.PlaylistTracks)
                .FirstOrDefaultAsync(p => p.Id == playlistId);
            if (playlist == null)
            {
                return NotFound();
            }

            playlist.PlaylistTracks.Add(new PlaylistTrack { PlaylistId = playlistId, Track = track });
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
        public async Task<IActionResult> EditPlaylist(int id, [FromBody] Playlist updatedPlaylist)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            playlist.Name = updatedPlaylist.Name;
            playlist.Description = updatedPlaylist.Description;
            await _context.SaveChangesAsync();
            return Ok(playlist);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlaylistById(int id)
        {
            var playlist = _context.Playlists.Find(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }
    }
}

using SoundScape.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class PlaylistTrack
{
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }
    [ForeignKey("PlaylistId")]
    public Playlist Playlist { get; set; }
    [ForeignKey("TrackId")]
    public Track Track { get; set; }
}

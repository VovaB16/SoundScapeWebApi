namespace SoundScape.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
        public User Owner { get; set; }
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public string Description { get; set; }
    }
}

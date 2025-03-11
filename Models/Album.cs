namespace SoundScape.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public string ImageUrl { get; set; }
    }
}
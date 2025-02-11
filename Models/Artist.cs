namespace SoundScape.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ArtistPopularity> ArtistPopularities { get; set; }
    }
}

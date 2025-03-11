namespace SoundScape.Models
{
    public class ArtistPopularity
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int Listens { get; set; }
        public int Likes { get; set; }
    }

}

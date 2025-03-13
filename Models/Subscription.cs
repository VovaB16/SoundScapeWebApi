namespace SoundScape.Models
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }

}

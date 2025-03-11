namespace SoundScape.DTOs
{
    public class UpdatePlaylistDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}

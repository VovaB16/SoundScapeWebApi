namespace SoundScape.DTOs
{
    public class CreateSingleDto
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string FilePath { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UpdateSingleDto
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string FilePath { get; set; }
        public string ImageUrl { get; set; }
    }

}

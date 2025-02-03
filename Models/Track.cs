using System;

namespace SoundScape.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

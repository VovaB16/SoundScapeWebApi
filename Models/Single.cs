﻿using SoundScape.Models;

public class Single
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public string FilePath { get; set; }
    public string ImageUrl { get; set; }
}

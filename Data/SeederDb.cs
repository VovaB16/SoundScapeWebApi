using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundScape.Data;
using SoundScape.Models;
using System;
using System.Linq;

public static class Seeder
{
    public static void SeedAlbum(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            var artist = context.Artists.FirstOrDefault(a => a.Id == 13);
            if (artist != null)
            {

                if (!context.Albums.Any(a => a.ArtistId == 13))
                {

                    var albums = new[]
                    {
                        new Album
                        {
                            Title = "The tortured poets department: the anthology",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = 13,
                            ImageUrl = "/images/ALTS1.png"
                        },
                        new Album
                        {
                            Title = "Album 2",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = 13,
                            ImageUrl = "/images/ALTSOD.png"
                        },
                        new Album
                        {
                            Title = "Album 3",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = 13,
                            ImageUrl = "/images/ALTSS.png"
                        },
                        new Album
                        {
                            Title = "Album 4",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = 13,
                            ImageUrl = "/images/ALTS1.png"
                        }
                    };

                    context.Albums.AddRange(albums);
                    context.SaveChanges();
                    Console.WriteLine("Albums added successfully.");
                }
            }
        }
    }

    public static void SeedSingle(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            var artist = context.Artists.FirstOrDefault(a => a.Id == 13);
            if (artist != null)
            {
                // Check if there are already singles for this artist
                if (!context.Singles.Any(s => s.ArtistId == 13))
                {
                    // Create 5 singles for the artist with ID 13
                    var singles = new[]
                    {
                        new Single
                        {
                            Title = "Single 1",
                            ReleaseDate = DateTime.UtcNow.AddDays(-10),
                            ArtistId = 13,
                            Genre = "Pop",
                            Duration = "3:30",
                            FilePath = "/path/to/single1.mp3",
                            ImageUrl = "/images/STSI.png"
                        },
                        new Single
                        {
                            Title = "Single 2",
                            ReleaseDate = DateTime.UtcNow.AddDays(-20),
                            ArtistId = 13,
                            Genre = "Rock",
                            Duration = "4:00",
                            FilePath = "/path/to/single2.mp3",
                            ImageUrl = "/images/STSI2.png"
                        },
                        new Single
                        {
                            Title = "Single 3",
                            ReleaseDate = DateTime.UtcNow.AddDays(-30),
                            ArtistId = 13,
                            Genre = "Jazz",
                            Duration = "5:00",
                            FilePath = "/path/to/single3.mp3",
                            ImageUrl = "/images/STSI.png"
                        },
                        new Single
                        {
                            Title = "Single 4",
                            ReleaseDate = DateTime.UtcNow.AddDays(-40),
                            ArtistId = 13,
                            Genre = "Hip-Hop",
                            Duration = "3:45",
                            FilePath = "/path/to/single4.mp3",
                            ImageUrl = "/images/STSI2.png"
                        },
                        new Single
                        {
                            Title = "Single 5",
                            ReleaseDate = DateTime.UtcNow.AddDays(-50),
                            ArtistId = 13,
                            Genre = "Classical",
                            Duration = "6:00",
                            FilePath = "/path/to/single5.mp3",
                            ImageUrl = "/images/STSI.png"
                        }
                    };

                    context.Singles.AddRange(singles);
                    context.SaveChanges();
                }
            }
        }
    }
}
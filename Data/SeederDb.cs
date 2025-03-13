using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundScape.Data;
using SoundScape.Models;
using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

public static class Seeder
{
    public static void SeedArtists(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (!context.Artists.Any())
            {
                var artists = new[]
                {
                    new Artist
                    {
                        Id = 22,
                        Name = "Drake",
                        ImageUrl = "/images/Drake.svg",
                        Albums = new List<Album>(),
                        ArtistPopularities = new List<ArtistPopularity>()
                    },
                    new Artist
                    {
                        Id = 23,
                        Name = "Billie Eilish",
                        ImageUrl = "/images/Bilie_Eilish.svg",
                        Albums = new List<Album>(),
                        ArtistPopularities = new List<ArtistPopularity>()
                    },
                    new Artist
                    {
                        Id = 24,
                        Name = "Bad Bunny",
                        ImageUrl = "/images/Bad_Bunny.svg",
                        Albums = new List<Album>(),
                        ArtistPopularities = new List<ArtistPopularity>()
                    },
                    new Artist
                    {
                        Id = 25,
                        Name = "Taylor Swift",
                        ImageUrl = "/images/Taylor_Swift.svg",
                        Albums = new List<Album>(),
                        ArtistPopularities = new List<ArtistPopularity>()
                    },
                    new Artist
                    {
                        Id = 26,
                        Name = "The Weeknd",
                        ImageUrl = "/images/The_Weeknd.svg",
                        Albums = new List<Album>(),
                        ArtistPopularities = new List<ArtistPopularity>()
                    }
                };

                context.Artists.AddRange(artists);
                context.SaveChanges();
                Console.WriteLine("Artists added successfully.");
            }
            else
            {
                Console.WriteLine("Artists already exist in the database.");
            }
        }
    }

    public static void SeedAlbumsForAllArtists(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            var artists = context.Artists.ToList();
            if (!artists.Any())
            {
                Console.WriteLine("No artists found to add albums.");
                return;
            }

            foreach (var artist in artists)
            {
                if (!context.Albums.Any(a => a.ArtistId == artist.Id))
                {
                    var albums = new[]
                    {
                        new Album
                        {
                            Title = $"{artist.Name} Album 1",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = artist.Id,
                            ImageUrl = $"/images/{artist.Name.Replace(" ", "_")}_album1.jpg"
                        },
                        new Album
                        {
                            Title = $"{artist.Name} Album 2",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = artist.Id,
                            ImageUrl = $"/images/{artist.Name.Replace(" ", "_")}_album2.jpg"
                        },
                        new Album
                        {
                            Title = $"{artist.Name} Album 3",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = artist.Id,
                            ImageUrl = $"/images/{artist.Name.Replace(" ", "_")}_album3.jpg"
                        },
                        new Album
                        {
                            Title = $"{artist.Name} Album 4",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = artist.Id,
                            ImageUrl = $"/images/{artist.Name.Replace(" ", "_")}_album4.jpg"
                        },
                        new Album
                        {
                            Title = $"{artist.Name} Album 5",
                            ReleaseDate = DateTime.UtcNow,
                            ArtistId = artist.Id,
                            ImageUrl = $"/images/{artist.Name.Replace(" ", "_")}_album5.jpg"
                        }
                    };

                    context.Albums.AddRange(albums);
                }
            }

            context.SaveChanges();
            Console.WriteLine("Albums added to all artists successfully.");
        }
    }

    public static void SeedTrack(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {

            if (!context.MusicTracks.Any())
            {
                var tracks = new[]
                {
                    

            //Drake
                new Track
                {
                    Title = "Drake 9",
                    Artist = "Drake",
                    Album = "Drake",
                    Genre = "Synthwave",
                    Duration = "3:20",
                    FilePath = "/tracks/Drake-9.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/drake.png"
                },
                new Track
                {
                    Title = "Chlds Play",
                    Artist = "Drake",
                    Album = "When We All Fall Asleep, Where Do We Go?",
                    Genre = "Electropop",
                    Duration = "3:14",
                    FilePath = "/tracks/Drake-ChildsPlay.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/drake.png"
                },
                new Track
                {
                    Title = "Controlla",
                    Artist = "Drake",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/Drake-Controlla.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/drake.png"
                },
                new Track
                {
                    Title = "Keep The Family Close",
                    Artist = "Drake",
                    Album = "El Último Tour Del Mundo",
                    Genre = "Reggaeton",
                    Duration = "3:25",
                    FilePath = "/tracks/Drake-KeepTheFamilyClose.mp3",
                    UploadDate = DateTime.UtcNow,
                   ImageUrl = "/images/drake.png"
                },





                //Billie Eilish 
                new Track
                {
                    Title = "bad guy",
                    Artist = "Billie Eilish",
                    Album = "Lover",
                    Genre = "Pop",
                    Duration = "3:41",
                    FilePath = "/tracks/BillieEilishbadguy.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/Billie-Eilish.jpg"
                },
                new Track
                {
                    Title = "Bored",
                    Artist = "Billie Eilish",
                    Album = "After Hours",
                    Genre = "Synthwave",
                    Duration = "3:20",
                    FilePath = "/tracks/BillieEilishBored.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/Billie-Eilish.jpg"
                },
                new Track
                {
                    Title = "hostage",
                    Artist = "Billie Eilish",
                    Album = "When We All Fall Asleep, Where Do We Go?",
                    Genre = "Electropop",
                    Duration = "3:14",
                    FilePath = "/tracks/BillieEilishhostage.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/Billie-Eilish.jpg"
                },
                new Track
                {
                    Title = "Khalid lovely",
                    Artist = "Billie Eilish",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/BillieEilishKhalidlovely.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/Billie-Eilish.jpg"
                },
                new Track
                {
                    Title = "No Time To Die",
                    Artist = "Billie Eilish",
                    Album = "El Último Tour Del Mundo",
                    Genre = "Reggaeton",
                    Duration = "3:25",
                    FilePath = "/tracks/BillieEilishNoTimeToDie.mp3",
                    UploadDate = DateTime.UtcNow,
                   ImageUrl = "/images/Billie-Eilish.jpg"
                },
                new Track
                {
                    Title = "watch",
                    Artist = "Billie Eilish",
                    Album = "El Último Tour Del Mundo",
                    Genre = "Reggaeton",
                    Duration = "3:25",
                    FilePath = "/tracks/BillieEilishwatch.mp3",
                    UploadDate = DateTime.UtcNow,
                  ImageUrl = "/images/Billie-Eilish.jpg"
                },




                //Bad Bunny
                new Track
                {
                    Title = "Amorfoda",
                    Artist = "Bad Bunny",
                    Album = "Lover",
                    Genre = "Pop",
                    Duration = "3:41",
                    FilePath = "/tracks/BadBunny-Amorfoda.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/BadBunny.jpg"
                },
                new Track
                {
                    Title = "Callaita",
                    Artist = "Bad Bunny",
                    Album = "After Hours",
                    Genre = "Synthwave",
                    Duration = "3:20",
                    FilePath = "/tracks/Bad_Bunny_Tainy-Callaita.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/BadBunny.jpg"
                },
                new Track
                {
                    Title = "Belvin",
                    Artist = "Bad Bunny",
                    Album = "When We All Fall Asleep, Where Do We Go?",
                    Genre = "Electropop",
                    Duration = "3:14",
                    FilePath = "/tracks/Bad_Bunny_Belvin.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/BadBunny.jpg"
                },
                



                //The Weeknd
                new Track
                {
                    Title = "Montreal",
                    Artist = "The Weeknd",
                    Album = "Lover",
                    Genre = "Pop",
                    Duration = "3:41",
                    FilePath = "/tracks/TheWeeknd-Montreal.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/The_Weekend.png"
                },
                new Track
                {
                    Title = "The Knowing",
                    Artist = "The Weeknd",
                    Album = "After Hours",
                    Genre = "Synthwave",
                    Duration = "3:20",
                    FilePath = "/tracks/TheWeeknd-The Knowing.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/The_Weekend.png"
                },
                new Track
                {
                    Title = "The Morning",
                    Artist = "The Weeknd",
                    Album = "When We All Fall Asleep, Where Do We Go?",
                    Genre = "Electropop",
                    Duration = "3:14",
                    FilePath = "/tracks/TheWeeknd-TheMorning.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/The_Weekend.png"
                },
                new Track
                {
                    Title = "The Zone ft. Drake",
                    Artist = "The Weeknd",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/TheWeeknd-TheZoneft.Drake.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/The_Weekend.png"
                },
                new Track
                {
                    Title = "Till Dawn (Here Comes The Sun)",
                    Artist = "The Weeknd",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/TheWeekndTillDawn.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/The_Weekend.png"
                },


                //Taylor Swift
                new Track
                {
                    Title = "I Think He Knows",
                    Artist = "Taylor Swift",
                    Album = "Lover",
                    Genre = "Pop",
                    Duration = "3:41",
                    FilePath = "/tracks/TaylorSwiftThinkHeKnows.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/TaylorSwift.jpg"
                },
                new Track
                {
                    Title = "You're Not Sorry",
                    Artist = "Taylor Swift",
                    Album = "After Hours",
                    Genre = "Synthwave",
                    Duration = "3:20",
                    FilePath = "/tracks/TaylorSwift_You're_Not_Sorry.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/TaylorSwift.jpg"
                },
                new Track
                {
                    Title = "Change",
                    Artist = "Taylor Swift",
                    Album = "When We All Fall Asleep, Where Do We Go?",
                    Genre = "Electropop",
                    Duration = "3:14",
                    FilePath = "/tracks/TaylorSwiftChange.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/TaylorSwift.jpg"
                },
                new Track
                {
                    Title = "Delicate",
                    Artist = "Taylor Swift",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/TaylorSwiftDelicate.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/TaylorSwift.jpg"
                },
                new Track
                {
                    Title = "Jump Then Fall",
                    Artist = "Taylor Swift",
                    Album = "Scorpion",
                    Genre = "Hip-Hop",
                    Duration = "3:19",
                    FilePath = "/tracks/TaylorSwiftJump_Then_Fall.mp3",
                    UploadDate = DateTime.UtcNow,
                    ImageUrl = "/images/TaylorSwift.jpg"
                }

            };


                context.MusicTracks.AddRange(tracks);
                context.SaveChanges();
                Console.WriteLine("Tracks added successfully.");
            }
            else
            {
                Console.WriteLine("Tracks already exist in the database.");

            }
        }
    }


    public static void SeedSingle(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            var artist = context.Artists.FirstOrDefault(a => a.Id == 25);
            if (artist != null)
            {
                if (!context.Singles.Any(s => s.ArtistId == 25))
                {
                    var singles = new[]
                    {
                        new Single
                        {
                            Title = "I can do it with a broken heart (Dombresky Remix)",
                            ReleaseDate = DateTime.UtcNow.AddDays(-10),
                            ArtistId = 25,
                            Genre = "Pop",
                            Duration = "3:30",
                            FilePath = "/path/to/single1.mp3",
                            ImageUrl = "/images/STSI.png"
                        },
                        new Single
                        {
                            Title = "I can do it with a broken heart",
                            ReleaseDate = DateTime.UtcNow.AddDays(-20),
                            ArtistId = 25,
                            Genre = "Rock",
                            Duration = "4:00",
                            FilePath = "/path/to/single2.mp3",
                            ImageUrl = "/images/STSI2.png"
                        },
                        new Single
                        {
                            Title = "Fortnight (Acoustic Version)",
                            ReleaseDate = DateTime.UtcNow.AddDays(-30),
                            ArtistId = 25,
                            Genre = "Jazz",
                            Duration = "5:00",
                            FilePath = "/path/to/single3.mp3",
                            ImageUrl = "/images/STSF.png"
                        },
                        new Single
                        {
                            Title = "Fortnight (feat. Post Malone) [BLOND:ISH Remix]",
                            ReleaseDate = DateTime.UtcNow.AddDays(-40),
                            ArtistId = 25,
                            Genre = "Hip-Hop",
                            Duration = "3:45",
                            FilePath = "/path/to/single4.mp3",
                            ImageUrl = "/images/STSFF.png"
                        },
                        new Single
                        {
                            Title = "You’re Losing Me (From The Vault)",
                            ReleaseDate = DateTime.UtcNow.AddDays(-50),
                            ArtistId = 25,
                            Genre = "Classical",
                            Duration = "6:00",
                            FilePath = "/path/to/single5.mp3",
                            ImageUrl = "/images/STSY.png"
                        }
                    };

                    context.Singles.AddRange(singles);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Singles already exist for the artist.");
                }
            }
            else
            {
                Console.WriteLine("Artist not found for seeding singles.");
            }
        }
    }
}

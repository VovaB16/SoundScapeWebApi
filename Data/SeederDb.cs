using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundScape.Data;
using SoundScape.Models;
using System;
using System.Linq;

public static class Seeder
{
    public static void SeedTracks(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.MusicTracks.Any())
            {
                return;   // DB has been seeded
            }

            var track1 = new Track
            {
                Title = "Test Track 1",
                Artist = "Test Artist",
                Album = "Test Album",
                Genre = "Test Genre",
                Duration = "3:30",
                FilePath = "/tracks/testtrack1.mp3",
                UploadDate = DateTime.UtcNow
            };

            var track2 = new Track
            {
                Title = "Test Track 2",
                Artist = "Test Artist",
                Album = "Test Album",
                Genre = "Test Genre",
                Duration = "4:00",
                FilePath = "/tracks/testtrack2.mp3",
                UploadDate = DateTime.UtcNow
            };

            context.MusicTracks.AddRange(track1, track2);
            context.SaveChanges();
        }
    }
}

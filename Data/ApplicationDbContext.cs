using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using SoundScape.Models;

namespace SoundScape.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Track> MusicTracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistPopularity> ArtistPopularities { get; set; }
        public DbSet<Single> Singles { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hashedPassword = "$2a$11$uvvUDzAYE8lerlmXzRmc3.CbNvJUiXEtawg.iNNcme3zmeepVZsJW";

            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     Id = 2,
                     Username = "admin",
                     Email = "admin@gmail.com",
                     PasswordHash = hashedPassword,
                     EmailConfirmed = true,
                     BirthDay = 1,
                     BirthMonth = 1,
                     BirthYear = 2000,
                     Gender = "Other",
                     AvatarUrl = "default_avatar_url",
                     Role = "Admin",
                 });



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistTrack>()
                .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

            modelBuilder.Entity<PlaylistTrack>()
                .HasOne(pt => pt.Playlist)
                .WithMany(p => p.PlaylistTracks)
                .HasForeignKey(pt => pt.PlaylistId);

            modelBuilder.Entity<PlaylistTrack>()
                .HasOne(pt => pt.Track)
                .WithMany()
                .HasForeignKey(pt => pt.TrackId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteTracks)
                .WithMany(t => t.FavoritedByUsers)
                .UsingEntity(j => j.ToTable("UserFavoriteTracks"));


            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.UserId, s.ArtistId });

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Subscribers)
                .HasForeignKey(s => s.ArtistId);
        }
    }
}
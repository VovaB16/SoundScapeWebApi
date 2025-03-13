using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using SoundScape.Models;

namespace SoundScape.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(j => j.ToTable("OrderProducts"));
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

    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime YearsOld { get; set; }

        public ApplicationUser(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
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


    }
}
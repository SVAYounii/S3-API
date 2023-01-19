using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace S3_Api_indi.Models
{
    public partial class MoviceComContext : DbContext, IMoviceComContext
    {
        public MoviceComContext()
        {
        }

        public MoviceComContext(DbContextOptions<MoviceComContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Content> Contents { get; set; } = null!;
        public virtual DbSet<ContentGenre> ContentGenres { get; set; } = null!;
        public virtual DbSet<Episode> Episodes { get; set; } = null!;
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Refreshtoken> Refreshtokens { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentGenre>(entity =>
            {
                entity.HasOne(d => d.Content)
                    .WithMany(p => p.ContentGenres)
                    .HasForeignKey(d => d.ContentId)
                    .HasConstraintName("FK_ContentGenre_Content");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.ContentGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_ContentGenre_Genre");
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_Episode_Season");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasOne(d => d.Content)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.ContentId)
                    .HasConstraintName("FK_Favourite_Content");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Favourite_Users");
            });

            modelBuilder.Entity<Refreshtoken>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Refreshtokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Refreshtoken_Users");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasOne(d => d.Content)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(d => d.ContentId)
                    .HasConstraintName("FK_Season_Content");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

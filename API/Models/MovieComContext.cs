using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace S3_Api_indi.Models
{
    public partial class MovieComContext : DbContext
    {
        public MovieComContext()
        {
        }

        public MovieComContext(DbContextOptions<MovieComContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost,1483;Database=MovieCom;User Id=sa;Password=Mocroboy6199!;");
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

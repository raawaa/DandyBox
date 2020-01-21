using Microsoft.EntityFrameworkCore;

namespace DandyBox.Core.DataModels
{
    public class DataContext : DbContext
    {
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Idol> Idols { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=dandybox.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Idol-Movie many-many relationship
            modelBuilder.Entity<IdolMovie>()
                .HasKey(t => new { t.IdolId, t.MovieId });

            modelBuilder.Entity<IdolMovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.IdolMovies)
                .HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<IdolMovie>()
                .HasOne(am => am.Idol)
                .WithMany(a => a.ActorMovies)
                .HasForeignKey(am => am.IdolId);

            // Movie-Genre many-many relationship
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // Movie's ProductId as uniqe index
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.ProductId)
                .IsUnique();

            // MediaFile - Movie relationship is optional
            modelBuilder.Entity<MediaFile>()
                .HasOne(mf => mf.Movie)
                .WithMany(m => m.MediaFiles)
                .IsRequired(false);
        }
    }
}
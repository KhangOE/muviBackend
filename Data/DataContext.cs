
using Microsoft.EntityFrameworkCore;
using movie.Models;

namespace movie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieCategory> MoviesCategories { get; set; }
        public DbSet<MovieDirector> MoviesDirectors { get; set;}
        public DbSet<MovieActor> MoviesActors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategory>()
                    .HasKey(pc => new { pc.MovieId, pc.CategoryId });
            modelBuilder.Entity<MovieCategory>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieCategories)
                    .HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<MovieCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.MovieCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<MovieActor>()
               .HasKey(pc => new { pc.MovieId, pc.ActorId });
            modelBuilder.Entity<MovieActor>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieActors)
                    .HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<MovieActor>()
                    .HasOne(p => p.Actor)
                    .WithMany(pc => pc.MovieActors)
                    .HasForeignKey(c => c.ActorId);

            modelBuilder.Entity<MovieDirector>()
              .HasKey(pc => new { pc.MovieId, pc.DirectorId });
            modelBuilder.Entity<MovieDirector>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieDirectors)
                    .HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<MovieDirector>()
                    .HasOne(p => p.Director)
                    .WithMany(pc => pc.MovieDirectors)
                    .HasForeignKey(c => c.DirectorId);





        }


    }
}

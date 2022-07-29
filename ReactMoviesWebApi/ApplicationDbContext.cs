using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MoviesActors>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            builder.Entity<MoviesGenres>()
                .HasKey(x => new { x.GenreId, x.MovieId });

            builder.Entity<MovieTheatersMovies>()
                .HasKey(x => new { x.MovieTheaterId, x.MovieId });

            base.OnModelCreating(builder);

            SeedRoles(builder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MovieTheatersMovies> MovieTheatersMovies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Entities.File> Files { get; set; }
        public DbSet<ActorPicture> ActorPictures { get; set; }
        public DbSet<MoviePoster> MoviePosters { get; set; }

        #region Seed Roles
        private async void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Manager Actors", ConcurrencyStamp = "2", NormalizedName = "Manager Actors" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Manager Genres", ConcurrencyStamp = "3", NormalizedName = "Manager Genres" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Manager Movies", ConcurrencyStamp = "4", NormalizedName = "Manager Movies" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Manager Movie Theaters", ConcurrencyStamp = "5", NormalizedName = "Manager Movie Theaters" }
            );
        }
        #endregion
    }
}

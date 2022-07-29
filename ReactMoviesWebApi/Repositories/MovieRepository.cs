using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containername = "movies";

        public MovieRepository(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        #region Create
        public async Task<Movie> CreateAsync(Movie movie, IFormFile poster)
        {
            movie.Id = Guid.NewGuid();

            if (poster != null)
            {
                movie.Poster = await _fileStorageService.SaveFile(containername, poster);
            }

            AnnotateActorsOrder(movie);
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<Movie>> GetAllOrderedByTitleAsync()
        {
            return await _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllUpcomingReleasesAsync(DateTime currentDate, int topRecords)
        {
            return await _context.Movies
                .Where(m => m.ReleaseDate > currentDate)
                .OrderBy(m => m.ReleaseDate)
                .Take(topRecords)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllInTheatersAsync(DateTime currentDate, int topRecords)
        {
            return await _context.Movies
                .Where(m => m.InTheaters)
                .OrderBy(m => m.ReleaseDate)
                .Take(topRecords)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(Guid id)
        {
            var movie = await _context.Movies
                .Include(m => m.MoviesGenres).ThenInclude(m => m.Genre)
                .Include(m => m.MovieTheatersMovies).ThenInclude(m => m.MovieTheater)
                .Include(m => m.MoviesActors).ThenInclude(m => m.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return null;
            }

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetByNameAsync(string title)
        {
            var movies = await _context.Movies
                .Where(m => m.Title.Contains(title))
                .OrderBy(m => m.Title)
                .Select(m => new Movie { Id = m.Id, Title = m.Title })
                .Take(5)
                .ToListAsync();

            if (movies == null)
            {
                return null;
            }

            return movies;
        }
        #endregion

        #region Update
        public async Task<Movie> UpdateAsync(Guid id, Movie movie, IFormFile poster)
        {
            var movieToUpdate = await _context.Movies
                .Include(mGenres => mGenres.MoviesGenres)
                .Include(mMovieTheaters => mMovieTheaters.MovieTheatersMovies)
                .Include(mActors => mActors.MoviesActors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieToUpdate == null)
            {
                return null;
            }

            movieToUpdate.Title = movie.Title;
            movieToUpdate.InTheaters = movie.InTheaters;
            movieToUpdate.ReleaseDate = movie.ReleaseDate;
            movieToUpdate.Trailer = movie.Trailer;
            movieToUpdate.Summary = movie.Summary;

            if (poster != null)
            {
                movieToUpdate.Poster = await _fileStorageService.EditFile(containername, poster, movieToUpdate.Poster);
            }

            movieToUpdate.MoviesGenres = movie.MoviesGenres;
            movieToUpdate.MovieTheatersMovies = movie.MovieTheatersMovies;
            movieToUpdate.MoviesActors = movie.MoviesActors;

            AnnotateActorsOrder(movieToUpdate);
            await _context.SaveChangesAsync();
            return movieToUpdate;

        }
        #endregion

        #region Delete
        public async Task<Movie> DeleteAsync(Guid id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return null;
            }

            _context.Remove(movie);
            await _fileStorageService.DeleteFile(movie.Poster, containername);
            await _context.SaveChangesAsync();
            return movie;
        }
        #endregion

        #region Private Methods

        private void AnnotateActorsOrder(Movie movie)
        {
            if (movie.MoviesActors != null)
            {
                for (var i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

        #endregion
    }
}

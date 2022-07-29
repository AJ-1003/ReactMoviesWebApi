using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class Movie_MovieTheatersRepository : IMovie_MovieTheatersRepository
    {
        private readonly ApplicationDbContext _context;

        public Movie_MovieTheatersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<MovieTheatersMovies> CreateAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public Task<IEnumerable<MovieTheatersMovies>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieTheatersMovies> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<MovieTheatersMovies> UpdateAsync(Guid id, MovieTheatersMovies mMovieTheater)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public Task<MovieTheatersMovies> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

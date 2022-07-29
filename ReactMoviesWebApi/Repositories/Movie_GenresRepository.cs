using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class Movie_GenresRepository : IMovie_GenresRepository
    {
        private readonly ApplicationDbContext _context;

        public Movie_GenresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<MoviesGenres> CreateAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public Task<IEnumerable<MoviesGenres>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MoviesGenres> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<MoviesGenres> UpdateAsync(Guid id, MoviesGenres mGenres)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public Task<MoviesGenres> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

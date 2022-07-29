using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<MovieTheater> CreateAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public Task<IEnumerable<MovieTheater>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieTheater> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<MovieTheater> UpdateAsync(Guid id, MovieTheater movieTheater)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public Task<MovieTheater> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

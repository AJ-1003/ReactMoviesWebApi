using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class Movie_ActorsRepository : IMovie_ActorsRepository
    {
        private readonly ApplicationDbContext _context;

        public Movie_ActorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<MoviesActors> CreateAsync(MoviesActors mActors)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public Task<IEnumerable<MoviesActors>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MoviesActors> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<MoviesActors> UpdateAsync(Guid id, MoviesActors mActors)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public Task<MoviesActors> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

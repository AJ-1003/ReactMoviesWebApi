using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<Rating> CreateAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public Task<IEnumerable<Rating>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Rating> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<Rating> UpdateAsync(Guid id, Rating rating)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public Task<Rating> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

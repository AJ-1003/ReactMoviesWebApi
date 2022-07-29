using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IRatingRepository
    {
        #region Create
        Task<Rating> CreateAsync();
        #endregion

        #region Read
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<Rating> UpdateAsync(Guid id, Rating rating);
        #endregion

        #region Delete
        Task<Rating> DeleteAsync(Guid id);
        #endregion
    }
}

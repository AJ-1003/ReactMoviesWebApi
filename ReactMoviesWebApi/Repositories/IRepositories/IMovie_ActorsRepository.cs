using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IMovie_ActorsRepository
    {
        #region Create
        Task<MoviesActors> CreateAsync(MoviesActors mActors);
        #endregion

        #region Read
        Task<IEnumerable<MoviesActors>> GetAllAsync();
        Task<MoviesActors> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<MoviesActors> UpdateAsync(Guid id, MoviesActors mActors);
        #endregion

        #region Delete
        Task<MoviesActors> DeleteAsync(Guid id);
        #endregion
    }
}

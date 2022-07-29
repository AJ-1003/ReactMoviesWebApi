using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IMovie_GenresRepository
    {
        #region Create
        Task<MoviesGenres> CreateAsync();
        #endregion

        #region Read
        Task<IEnumerable<MoviesGenres>> GetAllAsync();
        Task<MoviesGenres> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<MoviesGenres> UpdateAsync(Guid id, MoviesGenres mGenres);
        #endregion

        #region Delete
        Task<MoviesGenres> DeleteAsync(Guid id);
        #endregion
    }
}

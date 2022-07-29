using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        #region Create
        Task<Genre> CreateAsync(Genre genre);
        #endregion

        #region Read
        Task<IEnumerable<Genre>> GetAllOrderedByNameAsync();
        Task<IEnumerable<Genre>> GetByNameAsync(string name);
        Task<Genre> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<Genre> UpdateAsync(Guid id, Genre genre);
        #endregion

        #region Delete
        Task<Genre> DeleteAsync(Guid id);
        #endregion
    }
}

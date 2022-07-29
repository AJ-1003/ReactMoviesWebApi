using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IMovieTheaterRepository
    {
        #region Create
        Task<MovieTheater> CreateAsync();
        #endregion

        #region Read
        Task<IEnumerable<MovieTheater>> GetAllAsync();
        Task<MovieTheater> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<MovieTheater> UpdateAsync(Guid id, MovieTheater movieTheater);
        #endregion

        #region Delete
        Task<MovieTheater> DeleteAsync(Guid id);
        #endregion
    }
}

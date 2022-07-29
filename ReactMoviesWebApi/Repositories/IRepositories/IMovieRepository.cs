using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        #region Create
        Task<Movie> CreateAsync(Movie movie, IFormFile poster);
        #endregion

        #region Read
        Task<IEnumerable<Movie>> GetAllOrderedByTitleAsync();
        Task<IEnumerable<Movie>> GetByNameAsync(string title);
        Task<IEnumerable<Movie>> GetAllUpcomingReleasesAsync(DateTime currentDate, int topRecords);
        Task<IEnumerable<Movie>> GetAllInTheatersAsync(DateTime currentDate, int topRecords);
        Task<Movie> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<Movie> UpdateAsync(Guid id, Movie movie, IFormFile poster);
        #endregion

        #region Delete
        Task<Movie> DeleteAsync(Guid id);
        #endregion
    }
}

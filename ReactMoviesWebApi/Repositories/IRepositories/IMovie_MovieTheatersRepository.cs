using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IMovie_MovieTheatersRepository
    {
        #region Create
        Task<MovieTheatersMovies> CreateAsync();
        #endregion

        #region Read
        Task<IEnumerable<MovieTheatersMovies>> GetAllAsync();
        Task<MovieTheatersMovies> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<MovieTheatersMovies> UpdateAsync(Guid id, MovieTheatersMovies mMovieTheater);
        #endregion

        #region Delete
        Task<MovieTheatersMovies> DeleteAsync(Guid id);
        #endregion
    }
}

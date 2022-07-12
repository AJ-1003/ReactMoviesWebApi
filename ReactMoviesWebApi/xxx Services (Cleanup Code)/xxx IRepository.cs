using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Services
{
    public interface IRepository
    {
        void AddGenre(Genre genre);
        Task<List<Genre>> GetAllGenres();
        Genre GetGenreById(int id);
    }
}

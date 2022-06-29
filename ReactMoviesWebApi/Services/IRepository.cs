using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Services
{
    public interface IRepository
    {
        List<Genre> GetAllGenres();
        Genre GetGenreById(int id);
    }
}

using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genres;

        public ILogger<InMemoryRepository> _logger { get; }

        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _genres = new List<Genre>()
            {
                new Genre() {Id = 1, Name = "Comedy"},
                new Genre() {Id = 2, Name = "Action"}
            };
            _logger = logger;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            _logger.LogInformation("Executing GetAllGenres...");
            await Task.Delay(1000);
            return _genres;
        }

        public Genre GetGenreById(int id)
        {
            return _genres.FirstOrDefault(genre => genre.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            genre.Id = _genres.Max(x => x.Id) + 1;
            _genres.Add(genre);
        }
    }
}

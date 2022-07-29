using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public async Task<Genre> CreateAsync(Genre genre)
        {
            genre.Id = Guid.NewGuid();
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<Genre>> GetAllOrderedByNameAsync()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetByNameAsync(string name)
        {
            var genres = await _context.Genres
                .Where(g => g.Name.Contains(name))
                .OrderBy(g => g.Name)
                .Select(g => new Genre { Id = g.Id, Name = g.Name })
                .Take(5)
                .ToListAsync();

            if (genres == null)
            {
                return null;
            }

            return genres;
        }

        public async Task<Genre> GetByIdAsync(Guid id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return null;
            }

            return genre;
        }
        #endregion

        #region Update
        public async Task<Genre> UpdateAsync(Guid id, Genre genre)
        {
            var genreToUpdate = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return null;
            }

            genreToUpdate.Name = genre.Name;

            await _context.SaveChangesAsync();
            return genreToUpdate;
        }
        #endregion

        #region Delete
        public async Task<Genre> DeleteAsync(Guid id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return null;
            }

            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        #endregion
    }
}

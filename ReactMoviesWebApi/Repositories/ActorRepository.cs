using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "actors";

        public ActorRepository(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        #region Create
        public async Task<Actor> CreateAsync(Actor actor, IFormFile picture)
        {
            actor.Id = Guid.NewGuid();

            if (picture != null)
            {
                actor.Picture = await _fileStorageService.SaveFile(containerName, picture);
            }

            await _context.AddAsync(actor);
            await _context.SaveChangesAsync();
            return actor;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<Actor>> GetAllOrderedByNameAsync()
        {
            return await _context.Actors.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(Guid id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return null;
            }

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetByNameAsync(string name)
        {
            var actors = await _context.Actors
                .Where(a => a.Name.Contains(name))
                .OrderBy(a => a.Name)
                .Select(a => new Actor { Id = a.Id, Name = a.Name, Picture = a.Picture })
                .Take(5)
                .ToListAsync();

            if (actors == null)
            {
                return null;
            }

            return actors;
        }
        #endregion

        #region Update
        public async Task<Actor> UpdateAsync(Guid id, Actor actor, IFormFile picture)
        {
            var actorToUpdate = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actorToUpdate == null)
            {
                return null;
            }

            actorToUpdate.Name = actor.Name;
            actorToUpdate.DateOfBirth = actor.DateOfBirth;
            actorToUpdate.Biography = actor.Biography;

            if (picture != null)
            {
                actorToUpdate.Picture = await _fileStorageService.EditFile(containerName, picture, actorToUpdate.Picture);
            }

            await _context.SaveChangesAsync();
            return actorToUpdate;
        }
        #endregion

        #region Delete
        public async Task<Actor> DeleteAsync(Guid id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return null;
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            await _fileStorageService.DeleteFile(actor.Picture, containerName);
            return actor;
        }
        #endregion
    }
}

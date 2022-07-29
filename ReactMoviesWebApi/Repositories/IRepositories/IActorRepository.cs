using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IActorRepository
    {
        #region Create
        Task<Actor> CreateAsync(Actor actor, IFormFile picture);
        #endregion

        #region Read
        Task<IEnumerable<Actor>> GetAllOrderedByNameAsync();
        Task<IEnumerable<Actor>> GetByNameAsync(string name);
        Task<Actor> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<Actor> UpdateAsync(Guid id, Actor actor, IFormFile picture);
        #endregion

        #region Delete
        Task<Actor> DeleteAsync(Guid id);
        #endregion
    }
}

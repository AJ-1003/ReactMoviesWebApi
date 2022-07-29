using Microsoft.AspNetCore.Identity;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        public Task<IdentityUser> AuthenticateUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}

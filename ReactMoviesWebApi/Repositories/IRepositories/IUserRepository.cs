using Microsoft.AspNetCore.Identity;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IUserRepository
    {
        #region Create
        Task<IdentityUser> AuthenticateUserAsync(string username, string password);
        #endregion

        #region Read

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}

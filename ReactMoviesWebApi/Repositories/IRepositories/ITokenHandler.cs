using Microsoft.AspNetCore.Identity;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface ITokenHandler
    {
        #region Create
        Task<string> CreateTokenAsync(IdentityUser user);
        #endregion
    }
}

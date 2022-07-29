using Microsoft.AspNetCore.Identity;
using ReactMoviesWebApi.DTO;

namespace ReactMoviesWebApi.Repositories.IRepositories
{
    public interface IAccountsRepository
    {
        #region Create
        Task<UserManagerResponseDTO> RegisterAsync(UserCredentialsDTO userCredentials);
        Task<AuthenticationResponseDTO> LoginAsync(UserCredentialsDTO userCredentials);
        Task<IdentityUser> MakeAdminAsync(Guid id);
        Task<IdentityUser> RemoveAdminAsync(Guid id);
        #endregion

        #region Read
        Task<IEnumerable<IdentityUser>> GetAllAsync();
        Task<IdentityUser> GetByIdAsync(Guid id);
        #endregion

        #region Update
        Task<IdentityUser> UpdateAsync(Guid id, string email);
        #endregion

        #region Delete
        Task<IdentityUser> DeleteUserAsync(Guid id);
        #endregion
    }
}

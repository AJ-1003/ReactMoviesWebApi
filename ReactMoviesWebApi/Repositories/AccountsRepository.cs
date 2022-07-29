using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Repositories.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactMoviesWebApi.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        #region Create
        public async Task<UserManagerResponseDTO> RegisterAsync(UserCredentialsDTO userCredentials)
        {
            var userExists = await _userManager.FindByEmailAsync(userCredentials.Email);

            if (userExists != null)
            {
                return new UserManagerResponseDTO
                {
                    Message = "User already exists!",
                    IsSuccess = false
                };
            }

            if (userCredentials == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            var user = new IdentityUser
            {
                Email = userCredentials.Email,
                UserName = userCredentials.Email
            };

            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                //TODO: Send confirmation email
                return new UserManagerResponseDTO
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }

            return new UserManagerResponseDTO
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<AuthenticationResponseDTO> LoginAsync(UserCredentialsDTO userCredentials)
        {
            var user = await _userManager.FindByEmailAsync(userCredentials.Email);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, userCredentials.Password);

            if (!result)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userCredentials.Email)
            };

            var claimsDB = await _userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var expiration = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var authToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthenticationResponseDTO
            {
                Token = authToken,
                Expiration = expiration
            };
        }

        public async Task<IdentityUser> MakeAdminAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userSetToAdminRole = await _userManager.AddToRoleAsync(user, "Admin");
            var userSetToAdminRoleClaim = await _userManager.AddClaimAsync(user, new Claim("role", "Admin"));

            if (userSetToAdminRole.Succeeded && userSetToAdminRoleClaim.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<IdentityUser> RemoveAdminAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userRemovedFromAdminRole = await _userManager.RemoveFromRoleAsync(user, "Admin");
            var userRemovedFromAdminRoleClaim = await _userManager.RemoveClaimAsync(user, new Claim("role", "Admin"));

            if (userRemovedFromAdminRole.Succeeded && userRemovedFromAdminRoleClaim.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Read
        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return null;
            }

            return user;
        }
        #endregion

        #region Update
        public async Task<IdentityUser> UpdateAsync(Guid id, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return null;
            }

            user.Email = email;
            await _context.SaveChangesAsync();
            return user;
        }
        #endregion

        #region Delete
        public async Task<IdentityUser> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReactMoviesWebApi.Repositories.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactMoviesWebApi.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        #region Create
        public async Task<string> CreateTokenAsync(IdentityUser identityUser)
        {
            // Create Claims
            var claims = new List<Claim>()
            {
                new Claim("email", identityUser.Email)
            };

            var user = await _userManager.FindByEmailAsync(identityUser.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            // Create Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Create Credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Token
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(120),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: credentials
                    );

            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}

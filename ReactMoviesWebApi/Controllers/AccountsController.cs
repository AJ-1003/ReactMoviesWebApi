using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Repositories.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ITokenHandler _tokenHandler;

        public AccountsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IMapper mapper,
            IAccountsRepository accountsRepository,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _accountsRepository = accountsRepository;
            _tokenHandler = tokenHandler;
        }

        #region Create
        [HttpPost("makeAdmin")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] Guid id)
        {
            var user = await _accountsRepository.MakeAdminAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("removeAdmin")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] Guid id)
        {
            var user = await _accountsRepository.RemoveAdminAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserCredentialsDTO userCredentials)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountsRepository.RegisterAsync(userCredentials);

                if (user.IsSuccess)
                {
                    return Ok(user);
                }

                return BadRequest(user);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login([FromBody] UserCredentialsDTO userCredentials)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountsRepository.LoginAsync(userCredentials);

                if (result != null)
                {
                    return result;
                }
            }

            return BadRequest("Incorrect email or password");
        }
        #endregion

        #region Read
        [HttpGet("listUsers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<List<IdentityUser>>> GetListUsers([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Users.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var users = await queryable.OrderBy(u => u.Email).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<IdentityUser>>(users);
        }
        #endregion

        #region Private Methods
        private async Task<AuthenticationResponseDTO> BuildToken(UserCredentialsDTO userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var user = await _userManager.FindByNameAsync(userCredentials.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(5);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

            return new AuthenticationResponseDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
        #endregion
    }
}

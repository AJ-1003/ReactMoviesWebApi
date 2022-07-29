using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Create

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody] RatingDTO ratingDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var currentRating = await _context.Ratings.FirstOrDefaultAsync(cr => cr.MovieId == ratingDTO.MovieId && cr.UserId == userId);

            if (currentRating == null)
            {
                var rating = new Rating();
                rating.MovieId = ratingDTO.MovieId;
                rating.Rate = ratingDTO.Rating;
                rating.UserId = userId;
                _context.Ratings.Add(rating);
            }
            else
            {
                currentRating.Rate = ratingDTO.Rating;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion
    }
}

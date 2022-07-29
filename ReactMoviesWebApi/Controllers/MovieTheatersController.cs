using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MovieTheatersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieTheatersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Create

        [HttpPost]
        public async Task<ActionResult> Post(CreateUpdate_MovieTheaterDTO movieTheaterCreationDTO)
        {
            var movieTheater = _mapper.Map<MovieTheater>(movieTheaterCreationDTO);
            _context.Add(movieTheater);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Read

        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.MovieTheaters.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var movieTheaters = await queryable.OrderBy(mt => mt.Name).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(Guid id)
        {
            var movieTheater = await _context.MovieTheaters.FirstOrDefaultAsync(mt => mt.Id == id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieTheaterDTO>(movieTheater);
        }

        #endregion

        #region Update

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, CreateUpdate_MovieTheaterDTO movieTheaterCreationDTO)
        {
            var movieTheater = await _context.MovieTheaters.FirstOrDefaultAsync(mt => mt.Id == id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            movieTheater = _mapper.Map(movieTheaterCreationDTO, movieTheater);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var movieTheater = await _context.MovieTheaters.FirstOrDefaultAsync(mt => mt.Id == id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            _context.Remove(new MovieTheater() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #endregion
    }
}

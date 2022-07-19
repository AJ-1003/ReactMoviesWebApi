using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Filters;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Services;

namespace ReactMoviesWebApi.Controllers
{
    [Route("api/genres")]
    // OR
    //[Route("api/[controller]")]
    [ApiController]
    /*
        This replaces the need to have: 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        in each method to return a BadRequest if an error occurred
     */
    [EnableCors]
    public class GenresController : ControllerBase
    {
        // ------------------------------< CLEANUP CODE >------------------------------
        //private readonly IRepository _repository;
        // ------------------------------< CLEANUP CODE >------------------------------

        private readonly ILogger<GenresController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(/*IRepository repository, */ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //_repository = repository;
            // ------------------------------< CLEANUP CODE >------------------------------

            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]               // api/genres
        //[EnableCors]
        // ------------------------------< CLEANUP CODE >------------------------------
        //[HttpGet("list")]       // api/genres/list
        //[HttpGet("/allgenres")] // overrides the Route attribute: api/allgenres
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // ------------------------------< CLEANUP CODE >------------------------------
        public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //_logger.LogInformation("Getting all genres...");
            //return await _repository.GetAllGenres();
            // ------------------------------< CLEANUP CODE >------------------------------

            var queryable = _context.Genres.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var genres = await queryable.OrderBy(genre => genre.Name).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("all")]               // api/genres
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var genres = await _context.Genres.OrderBy(genre => genre.Name).ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("{id:int}")]
        // ------------------------------< CLEANUP CODE >------------------------------
        // this get adds to the Route attribute
        // api/genres/id
        //[ServiceFilter(typeof(MyActionFilter))]
        // ------------------------------< CLEANUP CODE >------------------------------
        public async Task<ActionResult<GenreDTO>> Get(int id/*, [BindRequired] string name*/)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //_logger.LogDebug("Get by Id method executing...");

            //var genre = _repository.GetGenreById(id);

            //if (genre == null)
            //{
            //    _logger.LogWarning($"Genre with id {id} not found!");
            //    //throw new ApplicationException(); // will be caught by the MyExceptionFilter
            //    return NotFound();
            //}

            //return genre;
            // ------------------------------< CLEANUP CODE >------------------------------

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return _mapper.Map<GenreDTO>(genre);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //_repository.AddGenre(genre);

            //return NoContent();
            // ------------------------------< CLEANUP CODE >------------------------------

            var genre = _mapper.Map<Genre>(genreCreationDTO);
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //return NoContent();
            // ------------------------------< CLEANUP CODE >------------------------------

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            genre = _mapper.Map(genreCreationDTO, genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            // ------------------------------< CLEANUP CODE >------------------------------
            //return NoContent();
            // ------------------------------< CLEANUP CODE >------------------------------

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Remove(new Genre() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

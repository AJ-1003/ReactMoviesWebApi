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
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;

        public GenresController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper, IGenreRepository genreRepository)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        #region Create

        [HttpPost]
        public async Task<ActionResult<GenreDTO>> CreateAsync(CreateUpdate_GenreDTO createGenre)
        {
            // Convert DTO to entity object
            var genre = _mapper.Map<Genre>(createGenre);

            // Pass entity object to repository
            genre = await _genreRepository.CreateAsync(genre);

            // Convert entity object to DTO
            var genreDTO = _mapper.Map<GenreDTO>(genre);

            // Return response
            return genreDTO;
        }

        #endregion

        #region Read

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetAllAsync([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Genres.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var genres = await queryable.OrderBy(g => g.Name).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GenreDTO>>> GetAllOrderedByNameAsync()
        {
            // Get records from repository
            var genres = await _genreRepository.GetAllOrderedByNameAsync();

            // Return response
            return _mapper.Map<List<GenreDTO>>(genres);

        }

        [HttpGet("searchByName/{query}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GenreDTO>>> GetByNameAsync(string query)
        {
            // Pass query to repository
            var genres = await _genreRepository.GetByNameAsync(query);

            // Handle null
            if (genres == null)
            {
                return NotFound();
            }

            // Covert entity object to DTO
            var genreDTO = _mapper.Map<List<GenreDTO>>(genres);

            // Return response
            return genreDTO;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GenreDTO>> GetByIdAsync(Guid id)
        {
            // Get entity object from DB with id
            var genre = await _genreRepository.GetByIdAsync(id);

            // Handle null
            if (genre == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var genreDTO = _mapper.Map<GenreDTO>(genre);

            // Retrun response
            return genreDTO;
        }

        #endregion

        #region Update

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GenreDTO>> UpdateAsync(Guid id, [FromBody] CreateUpdate_GenreDTO updateGenre)
        {
            // Conver DTO to entity object
            var genre = _mapper.Map<Genre>(updateGenre);

            // Pass entity object to repository
            genre = await _genreRepository.UpdateAsync(id, genre);

            // Handle null
            if (genre == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var genreDTO = _mapper.Map<GenreDTO>(genre);

            // Return response
            return genreDTO;
        }

        #endregion

        #region Delete

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<GenreDTO>> DeleteAsync(Guid id)
        {
            // Get entity object from DB with id
            var genre = await _genreRepository.DeleteAsync(id);

            // Handle null
            if (genre == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var genreDTO = _mapper.Map<GenreDTO>(genre);

            // Return response
            return genreDTO;
        }

        #endregion
    }
}

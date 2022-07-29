using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Repositories.IRepositories;
using System.Text;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IActorRepository _actorRepository;

        public ActorsController(ApplicationDbContext context, IMapper mapper, IActorRepository actorRepository)
        {
            _context = context;
            _mapper = mapper;
            _actorRepository = actorRepository;
        }

        #region Create

        [HttpPost]
        public async Task<ActionResult<ActorDTO>> CreateAsync([FromForm] CreateUpdate_ActorDTO createActor)
        {
            // Convert DTO to entity object
            var actor = _mapper.Map<Actor>(createActor);

            // Pass entity object to repository
            actor = await _actorRepository.CreateAsync(actor, createActor.Picture);

            // Convert entity object to DTO
            var actorDTO = _mapper.Map<ActorDTO>(actor);

            // Return response
            return actorDTO;
        }

        #endregion

        #region Read

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> GetAllAsync([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var actors = await queryable.OrderBy(a => a.Name).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<ActorDTO>>(actors);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ActorDTO>>> GetAllOrderedByNameAsync()
        {
            // Get records from repository
            var actors = await _actorRepository.GetAllOrderedByNameAsync();

            // Return response
            return _mapper.Map<List<ActorDTO>>(actors);

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ActorDTO>> GetByIdAsync(Guid id)
        {
            // Get entity object from DB with id
            var actor = await _actorRepository.GetByIdAsync(id);

            // Handle null
            if (actor == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var actorDTO = _mapper.Map<ActorDTO>(actor);

            // Retrun response
            return actorDTO;
        }

        [HttpGet("searchByName/{query}")]
        public async Task<ActionResult<List<ActorsMovieDTO>>> GetByNameAsync(string query)
        {
            // Pass query to repository
            var actors = await _actorRepository.GetByNameAsync(query);

            // Handle null
            if (actors == null)
            {
                return NotFound();
            }

            // Covert entity object to DTO
            var actorsDTO = _mapper.Map<List<ActorsMovieDTO>>(actors.ToList());

            // Return response
            return actorsDTO;
        }

        #endregion

        #region Update

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ActorDTO>> UpdateAsync(Guid id, [FromForm] CreateUpdate_ActorDTO updateActor)
        {
            // Conver DTO to domain object
            var actor = _mapper.Map<Actor>(updateActor);

            // Pass entity object to repository
            actor = await _actorRepository.UpdateAsync(id, actor, updateActor.Picture);

            // Handle null
            if (actor == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var actorDTO = _mapper.Map<ActorDTO>(actor);

            // Return response
            return actorDTO;
        }

        #endregion

        #region Delete

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ActorDTO>> DeleteAsync(Guid id)
        {
            // Get entity object from DB with id
            var actor = await _actorRepository.DeleteAsync(id);

            // Handle null
            if (actor == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var actorDTO = _mapper.Map<ActorDTO>(actor);

            // Return response
            return actorDTO;
        }

        #endregion

        #region Private Methods - (NOT IN USE)

        private string ReadAsString(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    result.Append(reader.ReadLine());
                }
            }
            return result.ToString();
        }

        #endregion
    }
}

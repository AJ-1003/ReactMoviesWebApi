using Microsoft.AspNetCore.Mvc;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Services;

namespace ReactMoviesWebApi.Controllers
{
    [Route("api/genres")]
    // OR
    //[Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IRepository _repository;

        public GenresController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]               // api/genres
        [HttpGet("list")]       // api/genres/list
        [HttpGet("/allgenres")] // overrides the Route attribute: api/allgenres
        public ActionResult<List<Genre>> Get()
        {
            return _repository.GetAllGenres();
        }

        [HttpGet("{id:int}/{name=Ian}")]
        // this get adds to the Route attribute
        // api/genres/id
        public ActionResult<Genre> Get(int id, string name)
        {
            var genre = _repository.GetGenreById(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public ActionResult Post()
        {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put()
        {
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}

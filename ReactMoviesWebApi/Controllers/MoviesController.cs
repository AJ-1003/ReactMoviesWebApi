using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;
using ReactMoviesWebApi.Repositories.IRepositories;

namespace ReactMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMovieRepository _movieRepository;
        private string container = "movies";

        public MoviesController(ApplicationDbContext context,
            IMapper mapper,
            IFileStorageService fileStorageService,
            UserManager<IdentityUser> userManager,
            IMovieRepository movieRepository)
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _userManager = userManager;
            _movieRepository = movieRepository;
        }

        #region Create

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> CreateAsync([FromForm] CreateUpdate_MovieDTO createMovie)
        {
            //var movie = _mapper.Map<Movie>(createMovie);

            //if (createMovie.Poster != null)
            //{
            //    movie.Poster = await _fileStorageService.SaveFile(container, createMovie.Poster);
            //}

            //AnnotateActorsOrder(movie);
            //_context.Add(movie);
            //await _context.SaveChangesAsync();
            //return NoContent();


            // Convert DTO to entity object
            var movie = _mapper.Map<Movie>(createMovie);

            // Pass entity object to repository
            movie = await _movieRepository.CreateAsync(movie, createMovie.Poster);

            // Convert entity object to DTO
            var movieDTO = _mapper.Map<MovieDTO>(movie);

            // Return response
            return movieDTO;
        }

        #endregion

        #region Read

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<LandingPageDTO>> GetAllOrderedByTitleAsync()
        {
            //var upcomingReleases = await _context.Movies
            //    .Where(m => m.ReleaseDate > today)
            //    .OrderBy(m => m.ReleaseDate)
            //    .Take(top)
            //    .ToListAsync();

            //var inTheaters = await _context.Movies
            //    .Where(m => m.InTheaters)
            //    .OrderBy(m => m.ReleaseDate)
            //    .Take(top)
            //    .ToListAsync();

            //var landingPageDTO = new LandingPageDTO();
            //landingPageDTO.UpcomingReleases = _mapper.Map<List<MovieDTO>>(upcomingReleases);
            //landingPageDTO.InTheaters = _mapper.Map<List<MovieDTO>>(inTheaters);

            //return landingPageDTO;


            // Figure out steps
            // Top records to display
            var topRecords = 6;

            // Specify current date to compare against release dates
            var currentDate = DateTime.Today;

            // Get upcoming releases from repository
            var upcomingReleases = await _movieRepository.GetAllUpcomingReleasesAsync(currentDate, topRecords);

            // Get intheaters from repository
            var inTheaters = await _movieRepository.GetAllInTheatersAsync(currentDate, topRecords);

            // Map records to landingPageDTO
            var landingPageDTO = new LandingPageDTO();
            landingPageDTO.UpcomingReleases = _mapper.Map<List<MovieDTO>>(upcomingReleases);
            landingPageDTO.InTheaters = _mapper.Map<List<MovieDTO>>(inTheaters);

            // Return response
            return landingPageDTO;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieDTO>> GetByIdAsync(Guid id)
        {
            //var movie = await _context.Movies
            //    .Include(m => m.MoviesGenres).ThenInclude(m => m.Genre)
            //    .Include(m => m.MovieTheatersMovies).ThenInclude(m => m.MovieTheater)
            //    .Include(m => m.MoviesActors).ThenInclude(m => m.Actor)
            //    .FirstOrDefaultAsync(m => m.Id.Equals(id));

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //var averageVote = 0.0;
            //var userVote = 0;

            //if (await _context.Ratings.AnyAsync(mr => mr.MovieId == id))
            //{
            //    averageVote = await _context.Ratings
            //        .Where(r => r.MovieId == id)
            //        .AverageAsync(r => r.Rate);

            //    if (HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        var email = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "email").Value;
            //        var user = await _userManager.FindByEmailAsync(email);
            //        var userId = user.Id;

            //        var ratingDb = await _context.Ratings.FirstOrDefaultAsync(r => r.MovieId == id && r.UserId == userId);

            //        if (ratingDb != null)
            //        {
            //            userVote = ratingDb.Rate;
            //        }
            //    }
            //}

            //var dto = _mapper.Map<MovieDTO>(movie);
            //dto.AverageVote = averageVote;
            //dto.UserVote = userVote;
            //dto.Actors = dto.Actors.OrderBy(a => a.Order).ToList();
            //return dto;


            // Get entity object from DB with id
            var movie = await _movieRepository.GetByIdAsync(id);

            // Handle null
            if (movie == null)
            {
                return NotFound();
            }

            // Check ratings
            var averageVote = 0.0;
            var userVote = 0;

            if (await _context.Ratings.AnyAsync(mr => mr.MovieId == id))
            {
                averageVote = await _context.Ratings
                    .Where(r => r.MovieId == id)
                    .AverageAsync(r => r.Rate);

                if (averageVote <= 0)
                {
                    averageVote = 0.0;
                }

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var email = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "email").Value;
                    var user = await _userManager.FindByEmailAsync(email);
                    var userId = user.Id;

                    var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.MovieId == id && r.UserId == userId);

                    if (rating != null)
                    {
                        userVote = rating.Rate;
                    }
                }
            }

            // Convert entity object to DTO
            var movieDTO = _mapper.Map<MovieDTO>(movie);
            movieDTO.AverageVote = averageVote;
            movieDTO.UserVote = userVote;
            movieDTO.Actors = movieDTO.Actors.OrderBy(a => a.Order).ToList();

            // Retrun response
            return movieDTO;
        }

        [HttpGet("searchByName/{query}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieDTO>>> GetByNameAsync(string query)
        {
            // Pass query to repository
            var movies = await _movieRepository.GetByNameAsync(query);

            // Handle null
            if (movies == null)
            {
                return NotFound();
            }

            // Covert entity object to DTO
            var movieDTO = _mapper.Map<List<MovieDTO>>(movies);

            // Return response
            return movieDTO;
        }

        [HttpGet("PutGet/{id:guid}")]
        public async Task<ActionResult<PutGetMovieDTO>> Update_GetDetailsAsync(Guid id)
        {
            // Check result from GetByIdAsync action
            var movieActionResult = await GetByIdAsync(id);

            // Handle not found
            if (movieActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            // Get GetByIdAsync value (MovieDTO)
            var movie = movieActionResult.Value;

            // Get selected and non-selected genres for movie
            var selectedGenreIds = movie.Genres
                .Select(g => g.Id)
                .ToList();
            var nonSelectedGenreIds = await _context.Genres
                .Where(g => !selectedGenreIds.Contains(g.Id))
                .ToListAsync();

            // Get selected and non-selected movie theaters for movie
            var selectedMovieTheaterIds = movie.MovieTheaters
                .Select(mt => mt.Id)
                .ToList();
            var nonSelectedMovieTheaterIds = await _context.MovieTheaters
                .Where(mt => !selectedMovieTheaterIds.Contains(mt.Id))
                .ToListAsync();

            // Convert entity objects to DTO
            var nonSelectedGenresDTO = _mapper.Map<List<GenreDTO>>(nonSelectedGenreIds);
            var nonSelectedMovieTheatersDTO = _mapper.Map<List<MovieTheaterDTO>>(nonSelectedMovieTheaterIds);

            // Return response
            var response = new PutGetMovieDTO();
            response.Movie = movie;
            response.SelectedGenres = movie.Genres;
            response.NonSelectedGenres = nonSelectedGenresDTO;
            response.SelectedMovieTheaters = movie.MovieTheaters;
            response.NonSelectedMovieTheaters = nonSelectedMovieTheatersDTO;
            response.Actors = movie.Actors;
            return response;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<PostGetMovieDTO>> Create_GetDetailsAsync()
        {
            // Get movie theaters and genre and order them by name
            var movieTheaters = await _context.MovieTheaters
                .OrderBy(mt => mt.Name)
                .ToListAsync();
            var genres = await _context.Genres
                .OrderBy(g => g.Name)
                .ToListAsync();

            // Convert entity objects to DTO
            var movieTheatersDTO = _mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
            var genresDTO = _mapper.Map<List<GenreDTO>>(genres);

            // Return response
            return new PostGetMovieDTO() { Genres = genresDTO, MovieTheaters = movieTheatersDTO };
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieDTO>>> FilterAsync([FromQuery] FilterMoviesDTO filterMovies)
        {
            // Get queryable movie object
            var moviesQueryable = _context.Movies.AsQueryable();

            // Check that query title is not empty
            if (!string.IsNullOrWhiteSpace(filterMovies.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(filterMovies.Title));
            }

            // Check if query movie is in theaters
            if (filterMovies.InTheaters)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InTheaters);
            }

            // Check if query movie is an upcoming release
            if (filterMovies.UpcomingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
            }

            // Check if query movie genre ids is NOT null
            if (filterMovies.GenreId != null)
            {
                moviesQueryable = moviesQueryable
                    .Where(m => m.MoviesGenres.Select(mg => mg.GenreId)
                    .Contains(filterMovies.GenreId));
            }

            // Paginate all valid records
            await HttpContext.InsertParametersPaginationInHeader(moviesQueryable);
            var movies = await moviesQueryable
                .OrderBy(m => m.Title)
                .Paginate(filterMovies.PaginationDTO)
                .ToListAsync();

            // Return response
            return _mapper.Map<List<MovieDTO>>(movies);
        }

        #endregion

        #region Update

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<MovieDTO>> UpdateAsync(Guid id, [FromForm] CreateUpdate_MovieDTO updateMovie)
        {

            //var movie = await _context.Movies
            //    .Include(m => m.MoviesActors)
            //    .Include(m => m.MoviesGenres)
            //    .Include(m => m.MovieTheatersMovies)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //movie = _mapper.Map(updateMovie, movie);

            //if (updateMovie.Poster != null)
            //{
            //    movie.Poster = await _fileStorageService.EditFile(container, updateMovie.Poster, movie.Poster);
            //}

            //AnnotateActorsOrder(movie);
            //await _context.SaveChangesAsync();
            //return NoContent();


            // Convert DTO to entity object
            var movie = _mapper.Map<Movie>(updateMovie);

            // Pass entity object to repository
            movie = await _movieRepository.UpdateAsync(id, movie, updateMovie.Poster);

            // Handle null
            if (movie == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var movieDTO = _mapper.Map<MovieDTO>(movie);

            // Return response
            return movieDTO;
        }

        #endregion

        #region Delete

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<MovieDTO>> DeleteAsync(Guid id)
        {
            //    var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            //    if (movie == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.Remove(movie);
            //    await _context.SaveChangesAsync();
            //    await _fileStorageService.DeleteFile(movie.Poster, container);
            //    return NoContent();


            // Get entity object from repository with id
            var movie = await _movieRepository.DeleteAsync(id);

            // Handle null
            if (movie == null)
            {
                return NotFound();
            }

            // Convert entity object to DTO
            var movieDTO = _mapper.Map<MovieDTO>(movie);

            // Return response
            return movieDTO;
        }

        #endregion
    }
}

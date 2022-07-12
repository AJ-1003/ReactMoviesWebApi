using Microsoft.AspNetCore.Mvc;
using ReactMoviesWebApi.Helpers;
using System.Reflection;

namespace ReactMoviesWebApi.DTO
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleasedDate { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        //[ModelBinder(BinderType = typeof(TypeBinder<List<Type>>))]
        public List<int> GenresIds { get; set; }
        //[ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        [BindProperty(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> MovieTheatersIds { get; set; }

        //[ModelBinder(BinderType = typeof(TypeBinder<List<MoviesActorsCreationDTO>>))]
        public List<MoviesActorsCreationDTO> Actors { get; set; }

        //public static ValueTask<MovieCreationDTO?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
        //{
        //    // parse any values required from the Request
        //    int.TryParse(httpContext.Request.Form["Id"], out var id);
        //    //var test = httpContext.Request.Form
        //    // return the CreateTicketDto
        //    return ValueTask.FromResult<MovieCreationDTO?>(
        //        new MovieCreationDTO()
        //        {
        //            Title = httpContext.Request.Form["Title"]
        //        }
        //    );
        //}
    }
}

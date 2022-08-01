using Microsoft.AspNetCore.Mvc;
using ReactMoviesWebApi.Entities;
using ReactMoviesWebApi.Helpers;
using System.Reflection;

namespace ReactMoviesWebApi.DTO
{
    public class CreateUpdate_MovieDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleasedDate { get; set; }
        public IFormFile Poster { get; set; }

        //[ModelBinder(BinderType = typeof(TypeBinder<List<Guid>>))]
        public List<Guid> GenresIds { get; set; }

        //[ModelBinder(BinderType = typeof(TypeBinder<List<Guid>>))]
        public List<Guid> MovieTheatersIds { get; set; }

        //[ModelBinder(BinderType = typeof(TypeBinder<List<CreateUpdate_MoviesActorsDTO>>))]
        public List<CreateUpdate_MoviesActorsDTO> Actors { get; set; }
    }
}

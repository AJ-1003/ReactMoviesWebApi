using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class MoviesActors
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        [StringLength(maximumLength: 75, ErrorMessage = "The {0} field cannot contain more than 75 characters!")]
        public string Character { get; set; }
        public int Order { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}

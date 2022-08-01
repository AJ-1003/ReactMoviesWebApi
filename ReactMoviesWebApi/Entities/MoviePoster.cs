namespace ReactMoviesWebApi.Entities
{
    public class MoviePoster
    {
        public Guid PosterId { get; set; }
        public Guid MovieId { get; set; }
        public IFormFile Poster { get; set; }
        public Movie Movie { get; set; }
    }
}

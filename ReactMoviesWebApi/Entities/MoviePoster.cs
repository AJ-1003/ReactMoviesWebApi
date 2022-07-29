namespace ReactMoviesWebApi.Entities
{
    public class MoviePoster
    {
        public Guid PosterId { get; set; }
        public Guid MovieId { get; set; }
        public File Poster { get; set; }
        public Movie Movie { get; set; }
    }
}

namespace ReactMoviesWebApi.Entities
{
    public class MovieTheatersMovies
    {
        public Guid MovieTheaterId { get; set; }
        public Guid MovieId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public Movie Movie { get; set; }
    }
}

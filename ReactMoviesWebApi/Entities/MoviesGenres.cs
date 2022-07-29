namespace ReactMoviesWebApi.Entities
{
    public class MoviesGenres
    {
        public Guid GenreId { get; set; }
        public Guid MovieId { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}

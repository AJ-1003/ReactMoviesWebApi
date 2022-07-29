namespace ReactMoviesWebApi.DTO
{
    public class PostGetMovieDTO
    {
        public List<GenreDTO> Genres { get; set; }
        public List<MovieTheaterDTO> MovieTheaters { get; set; }
    }
}

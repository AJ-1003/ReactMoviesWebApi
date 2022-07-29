using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.DTO
{
    public class RatingDTO
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public Guid MovieId { get; set; }
    }
}

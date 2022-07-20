using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class MovieTheater
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(maximumLength: 75)]
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}

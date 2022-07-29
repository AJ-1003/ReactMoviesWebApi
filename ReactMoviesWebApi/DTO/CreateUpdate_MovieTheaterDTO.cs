using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.DTO
{
    public class CreateUpdate_MovieTheaterDTO
    {
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(maximumLength: 75, ErrorMessage = "The {0} field cannot contain more than 75 characters!")]
        public string Name { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}

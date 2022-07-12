using ReactMoviesWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.DTO
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(50)]
        [FirstLetterUppercase] // Attribute validation
        public string Name { get; set; }
    }
}

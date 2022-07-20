using ReactMoviesWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} field cannot contain more than 50 characters!")]
        [FirstLetterUppercase] // Attribute validation
        public string Name { get; set; }
    }
}

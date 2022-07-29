using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.DTO
{
    public class UserCredentialsDTO
    {
        [Required(ErrorMessage = "The {0} field is required!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        public string Password { get; set; }
    }
}

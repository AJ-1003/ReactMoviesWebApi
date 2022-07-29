using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class Actor
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(maximumLength: 120, ErrorMessage = "The {0} field cannot contain more than 120 characters!")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
    }
}

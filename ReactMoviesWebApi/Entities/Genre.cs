using ReactMoviesWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class Genre/* : IValidatableObject*/
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(50)]
        [FirstLetterUppercase] // Attribute validation
        public string Name { get; set; }

        // ------------------------------< CLEANUP CODE >------------------------------
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        var firstLetter = Name[0].ToString();

        //        if (firstLetter != firstLetter.ToUpper())
        //        {
        //            yield return new ValidationResult("First letter should be uppercase", new string[] { nameof(Name) });
        //        }
        //    }
        //}
        // ------------------------------< CLEANUP CODE >------------------------------
    }
}

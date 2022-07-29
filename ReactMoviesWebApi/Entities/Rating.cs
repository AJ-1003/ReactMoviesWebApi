using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReactMoviesWebApi.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}

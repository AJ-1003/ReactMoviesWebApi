using System.Reflection;

namespace ReactMoviesWebApi.DTO
{
    public class MoviesActorsCreationDTO
    {
        public int Id { get; set; }
        public string Character { get; set; }

        //public static ValueTask<MoviesActorsCreationDTO?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
        //{
        //    // parse any values required from the Request
        //    int.TryParse(httpContext.Request.Form["Id"], out var id);
        //    //var test = httpContext.Request.Form
        //    // return the CreateTicketDto
        //    return ValueTask.FromResult<MoviesActorsCreationDTO?>(
        //        new MoviesActorsCreationDTO()
        //        {
        //            Id = id,
        //            Character = httpContext.Request.Form["Character"]
        //        }
        //    );
        //}
    }
}

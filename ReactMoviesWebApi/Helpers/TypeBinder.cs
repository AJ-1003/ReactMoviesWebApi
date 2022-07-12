using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ReactMoviesWebApi.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(propertyName);

            if (value == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            else
            {
                try
                {
                    var deserializedValue = JsonConvert.DeserializeObject<T>(value.FirstValue);
                    bindingContext.Result = ModelBindingResult.Success(deserializedValue);
                }
                catch (Exception)
                {
                    bindingContext.ModelState.TryAddModelError(propertyName, "The given value is not of the correct type!");
                }

                return Task.CompletedTask;
            }
        }

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

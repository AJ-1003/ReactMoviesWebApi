using Microsoft.AspNetCore.Mvc.Filters;

namespace ReactMoviesWebApi.Filters
{
    public class MyActionFilter : IActionFilter
    {
        public readonly ILogger<MyActionFilter> _logger;

        public MyActionFilter(ILogger<MyActionFilter> logger)
        {
            _logger = logger;
        }

        // before
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogWarning("OnActionExecuting...");
        }

        // after
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogWarning("OnActionExecuted...");
        }
    }
}

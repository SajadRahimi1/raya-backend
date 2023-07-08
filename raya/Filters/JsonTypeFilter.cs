using Microsoft.AspNetCore.Mvc.Filters;

public class JsonContentTypeFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Content-Type"))
        {
            // context.Result = new BadRequestObjectResult($"Content-Type header is missing.");
            return;
        }

        var contentType = context.HttpContext.Request.Headers["Content-Type"].ToString();
        if (!contentType.Contains("application/json"))
        {
            // context.Result = new UnsupportedMediaTypeObjectResult($"Content-Type '{contentType}' is not supported. The supported type is 'application/json'.");
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do nothing
    }
}
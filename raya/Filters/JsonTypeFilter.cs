using Microsoft.AspNetCore.Mvc.Filters;

public class JsonContentTypeFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Request.ContentType = "application/json";
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do nothing
    }
}
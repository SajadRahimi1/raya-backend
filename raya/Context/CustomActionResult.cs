using Microsoft.AspNetCore.Mvc;

public class ErrorModel
{
    public string? ErrorMessage { get; set; }
}

public class Result
{
    public int statusCodes { get; set; } = StatusCodes.Status200OK;

    public object? Data { get; set; }

    public string? Token { get; set; }

    public ErrorModel? ErrorMessage { get; set; }
}

public class CustomActionResult : IActionResult
{
    private readonly Result _result;
    public CustomActionResult(Result result)
    {
        _result = result;
    }

    Result GetResult => _result;

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_result.ErrorMessage ?? _result.Data)
        {
            StatusCode = _result.statusCodes,
        };

        await objectResult.ExecuteResultAsync(context);
    }
}
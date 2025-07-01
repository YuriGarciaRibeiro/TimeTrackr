using FluentResults;

namespace WebAPI.Extensions.ResultExtensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        var firstError = result.Errors.FirstOrDefault();
        var statusCode = firstError?.Metadata.TryGetValue("StatusCode", out var codeObj) == true && codeObj is int code
            ? code
            : 400;

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = "Operation failed",
            Detail = string.Join("; ", result.Errors.Select(e => e.Message))
        };

        return new ObjectResult(problem) { StatusCode = statusCode };
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new NoContentResult();

        var firstError = result.Errors.FirstOrDefault();
        var statusCode = firstError?.Metadata.TryGetValue("StatusCode", out var codeObj) == true && codeObj is int code
            ? code
            : 400;

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = "Operation failed",
            Detail = string.Join("; ", result.Errors.Select(e => e.Message))
        };

        return new ObjectResult(problem) { StatusCode = statusCode };
    }
}
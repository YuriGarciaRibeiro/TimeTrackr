using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Middleware;

public class CustomExecptionHandler
    (ILogger<CustomExecptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError($"Error occurred: {exception.Message}, StackTrace: {exception.StackTrace}, Time: {DateTime.UtcNow}");

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            ValidationException => 
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity
            ),
            _ => 
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };
        
        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path,
        };
        
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }
        
         await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
         return true;
    }
}
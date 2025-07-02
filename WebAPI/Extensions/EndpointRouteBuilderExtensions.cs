using System.Diagnostics.CodeAnalysis;
using WebApi.Extensions.ResultExtensions;

namespace WebApi.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder MapGet<TRequest, TResponse>(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern = "")
        where TRequest : IRequest<Result<TResponse>>, new()
    {
        return endpoints.MapGet(pattern, DefaultGetBehavior<TRequest, TResponse>)
            .Produces<TResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public static RouteHandlerBuilder MapPost<TRequest, TResponse>(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern = "",
        int produces = StatusCodes.Status200OK)
        where TRequest : IRequest<Result<TResponse>>
    {
        return endpoints.MapPost(pattern, DefaultBehavior<TRequest, TResponse>)
            .Produces<TResponse>(produces)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    public static RouteHandlerBuilder MapPut<TRequest>(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern)
        where TRequest : IRequest<Result>
    {
        return endpoints.MapPut(pattern, DefaultBehavior<TRequest>)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> DefaultBehavior<TRequest>(
        [FromBody] TRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        where TRequest : IRequest<Result>
    {
        var response = await sender.Send(request, cancellationToken);
        return response.ToIResult();
    }

    private static async Task<IResult> DefaultBehavior<TRequest, TResponse>(
        [FromBody] TRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        where TRequest : IRequest<Result<TResponse>>
    {
        var response = await sender.Send(request, cancellationToken);
        return response.ToIResult();
    }

    private static async Task<IResult> DefaultGetBehavior<TRequest, TResponse>(
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        where TRequest : IRequest<Result<TResponse>>, new()
    {
        var request = new TRequest();
        var response = await sender.Send(request, cancellationToken);
        return response.ToIResult();
    }
}

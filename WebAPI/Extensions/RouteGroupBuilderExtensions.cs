using System.Diagnostics.CodeAnalysis;
using WebApi.Extensions.ResultExtensions;

public static class RouteGroupBuilderExtensions
{
    public static RouteHandlerBuilder MapGet<TRequest, TResponse>(
        this RouteGroupBuilder group,
        [StringSyntax("Route")] string pattern = "")
        where TRequest : IRequest<Result<TResponse>>, new()
    {
        return group.MapGet(pattern, DefaultGetBehavior<TRequest, TResponse>)
            .Produces<TResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public static RouteHandlerBuilder MapPost<TRequest, TResponse>(
        this RouteGroupBuilder group,
        [StringSyntax("Route")] string pattern = "",
        int produces = StatusCodes.Status200OK)
        where TRequest : IRequest<Result<TResponse>>
    {
        return group.MapPost(pattern, DefaultBehavior<TRequest, TResponse>)
            .Produces<TResponse>(produces)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    public static RouteHandlerBuilder MapPut<TRequest>(
        this RouteGroupBuilder group,
        [StringSyntax("Route")] string pattern)
        where TRequest : IRequest<Result>
    {
        return group.MapPut(pattern, DefaultBehavior<TRequest>)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    public static RouteHandlerBuilder MapDelete<TRequest>(
        this RouteGroupBuilder group,
        [StringSyntax("Route")] string pattern)
        where TRequest : IRequest<Result>
    {
        return group.MapDelete(pattern, DefaultBehavior<TRequest>)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    // Reutilizado por POST, PUT, DELETE (sem resposta)
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

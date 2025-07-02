using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebApi.Extensions.ResultExtensions;

namespace WebApi.Extensions;

public static class RouteGroupBuilderExtensions
{
    public static RouteHandlerBuilder MapGet<TRequest, TResponse>(
        this RouteGroupBuilder group,
        [StringSyntax("Route")] string pattern = "")
        where TRequest : IRequest<Result<TResponse>>, new()
    {
        return group.MapGet(pattern, async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var request = new TRequest();
            var response = await sender.Send(request, cancellationToken);
            return response.ToIResult();
        })
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
        return group.MapPost(pattern, async ([FromBody] TRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.ToIResult();
        })
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
        return group.MapPut(pattern, async ([FromBody] TRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.ToIResult();
        })
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .ProducesValidationProblem();
    }
}

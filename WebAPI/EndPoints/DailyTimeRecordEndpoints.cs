using Application.UseCases.GetDailyTimeRecordByUser;
using Application.UseCases.RegisterDailyTimeRecord;
using WebApi.Extensions.ResultExtensions;

namespace WebAPI.EndPoints;

internal static class DailyTimeRecordEndpoints
{
    public static IEndpointRouteBuilder MapDailyTimeRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("DailyTimeRecord")
            .WithTags("DailyTimeRecord")
            .WithOpenApi();

        group.MapPost<RegisterDailyTimeRecordCommand, RegisterDailyTimeRecordResponse>("")
            .WithName("RegisterDailyTimeRecord")
            .WithSummary("Register a daily time record for a user");

        group.MapGet(
            "/user/{id:guid}",
            async (
                [FromRoute] Guid id,
                [FromServices] ISender sender,
                CancellationToken cancellationToken
            ) =>
            {
                var query = new GetDailyTimeRecordByUserQuery { UserId = id };
                var result = await sender.Send(query, cancellationToken);
                return result.ToIResult();
            }
        )
        .WithName("GetDailyTimeRecordByUser")
        .WithSummary("Obtém registros diários de ponto de um usuário pelo ID")
        .Produces<GetDailyTimeRecordByUserResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);


        return app;
    } 
}
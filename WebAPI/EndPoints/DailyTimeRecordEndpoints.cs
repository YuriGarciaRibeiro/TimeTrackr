using Application.UseCases.RegisterDailyTimeRecord;

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

        return app;
    } 
}
using Prometheus;
using WebAPI.EndPoints;

namespace WebAPI.Extensions;

public static class AppExtensions
{
    public static WebApplication UseMiddlewares(this WebApplication app)
    {
        return app;
    }

    public static WebApplication UserMetrics(this WebApplication app)
    {
        app.MapMetrics();
        app.MapGet("/", () => "API Online");
        return app;
    }
    
    public static WebApplication UseEndpoints(this WebApplication app)
    {
        app.MapUserEndpoints()
           .MapDailyTimeRecordEndpoints();
        return app;
    }
}
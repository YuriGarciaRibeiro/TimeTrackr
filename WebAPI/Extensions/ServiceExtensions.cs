using Application.Behaviors;
using Application.Services;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WebAPI.Middleware;


namespace WebAPI.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITimeRecordService, TimeRecordService>();
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        // ADD YOUR REPOSITORIES HERE
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IDailyTimeRecordRepository, DailyTimeRecordRepository>();
        return builder;
    }
    
    public static WebApplicationBuilder AddCustomExecptionHanlder(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<CustomExecptionHandler>();
        builder.Services.AddProblemDetails();
        return builder;
    }

    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        // ADD YOUR DATABASE CONTEXT HERE
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        return builder;
    }
    
    public static WebApplication ExecuteMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        return app;
    }


    public static WebApplicationBuilder AddMediatR(this WebApplicationBuilder builder)
    {   
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return builder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        // ADD YOUR SWAGGER HERE
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

}

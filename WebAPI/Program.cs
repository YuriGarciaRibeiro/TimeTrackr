using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.AddDatabase(builder.Configuration)
       .AddMediatR()
       .AddRepositories()
       .AddCustomExecptionHanlder()
       .AddServices();

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.AddSwagger();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.ExecuteMigrations()
        .UseSwagger()
        .UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseMiddlewares();

app.MapControllers();
app.UserMetrics();

app.Run();

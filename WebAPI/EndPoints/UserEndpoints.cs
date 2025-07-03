using Application.UseCases.DeleteUser;
using Application.UseCases.GetUsers;

namespace WebAPI.EndPoints;

internal static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Users") // removido "v{version:apiVersion}/" se não estiver usando versionamento
            .WithTags("User")
            .WithOpenApi();

        group.MapGet<GetUserQuery, GetUsersResponse>("")
            .WithName("GetUsers")
            .WithSummary("Get all users");

        group.MapPost<CreateUserCommand, CreateUserResponse>("")
            .WithName("CreateUser")
            .WithSummary("Create a new user");

        group.MapDelete<DeleteUserCommand>("")
            .WithName("DeleteUser")
            .WithSummary("Delete a user by ID");

        return app;
    }
}

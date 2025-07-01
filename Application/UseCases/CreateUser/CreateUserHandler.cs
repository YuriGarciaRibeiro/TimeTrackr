using Application.CQRS;
using Core.Entities;
using Core.Interfaces;
using FluentResults;
using MediatR;

namespace Application.UseCases.CreateUser;

public class CreateUserHandler
    (IUserRepository userRepository)
    : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role
        };

        var addResult = await userRepository.AddAsync(user);
        if (addResult.IsFailed)
            return Result.Fail(addResult.Errors);

        return Result.Ok(new CreateUserResponse { UserId = user.Id });
    }
}
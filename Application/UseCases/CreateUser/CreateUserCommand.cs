using Application.CQRS;
using Core;
using FluentResults;

namespace Application.UseCases.CreateUser;

public class CreateUserCommand(string name, string email, string password, UserRole role = UserRole.Employee)
    : ICommand<CreateUserResponse>
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public UserRole Role { get; set; } = role;
}
namespace Application.UseCases.CreateUser;

public class CreateUserCommand : ICommand<CreateUserResponse>
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public UserRole Role { get; set; } = UserRole.Employee;
}

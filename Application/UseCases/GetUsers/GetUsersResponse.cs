namespace Application.UseCases.GetUsers;

public class GetUsersResponse
{
    public List<UserDto> Users { get; set; } = new List<UserDto>();
}

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
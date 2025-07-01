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
    public List<DailyTimeRecordDto> DailyTimeRecords { get; set; } = new List<DailyTimeRecordDto>();
}

public class DailyTimeRecordDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }

    public TimeOnly? StartWork { get; set; }
    public TimeOnly? StartLunch { get; set; }
    public TimeOnly? EndLunch { get; set; }
    public TimeOnly? EndWork { get; set; }
}
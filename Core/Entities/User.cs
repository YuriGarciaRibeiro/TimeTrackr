namespace Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;

    public ICollection<DailyTimeRecord> TimeRecords { get; set; } = new List<DailyTimeRecord>();
}


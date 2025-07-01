namespace Core.Entities;

public class DailyTimeRecord
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateOnly Date { get; set; }

    public TimeOnly? StartWork { get; set; }
    public TimeOnly? StartLunch { get; set; }
    public TimeOnly? EndLunch { get; set; }
    public TimeOnly? EndWork { get; set; }

    public User Employee { get; set; } = null!;
}
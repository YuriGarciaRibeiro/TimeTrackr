namespace Application.UseCases.GetDailyTimeRecordByUser;

public class GetDailyTimeRecordByUserResponse
{
    public Guid UserId { get; set; }
    public List<TimeRecordResponse> TimeRecords { get; set; } = new List<TimeRecordResponse>();
}

public class TimeRecordResponse
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? StartWork { get; set; }
    public TimeOnly? StartLunch { get; set; }
    public TimeOnly? EndLunch { get; set; }
    public TimeOnly? EndWork { get; set; }
}



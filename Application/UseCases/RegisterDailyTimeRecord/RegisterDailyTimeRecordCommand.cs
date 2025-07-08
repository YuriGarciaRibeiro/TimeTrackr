namespace Application.UseCases.RegisterDailyTimeRecord;

public class RegisterDailyTimeRecordCommand : ICommand<RegisterDailyTimeRecordResponse>
{
    public Guid UserId { get; set; }
    public DateTime? Date { get; set; }
}

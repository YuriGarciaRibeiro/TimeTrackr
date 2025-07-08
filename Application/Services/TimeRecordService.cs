namespace Application.Services;

public class TimeRecordService : ITimeRecordService
{
    private readonly IDailyTimeRecordRepository _repository;

    public TimeRecordService(IDailyTimeRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> RegisterTimeAsync(Guid userId, DateTime now)
    {
        var today = DateOnly.FromDateTime(now);
        var existing = await _repository.GetDailyRecordsForUserAsync(userId);
        var record = existing.FirstOrDefault(r => r.Date == today);

        if (record == null)
        {
            await _repository.RegisterDailyTimeRecordAsync(userId, now, now.TimeOfDay);
            return Result.Ok().WithSuccess("Time registered: start of work.");
        }

        if (record.StartWork == null)
        {
            record.StartWork = TimeOnly.FromDateTime(now);
        }
        else if (record.StartLunch == null)
        {
            record.StartLunch = TimeOnly.FromDateTime(now);
        }
        else if (record.EndLunch == null)
        {
            record.EndLunch = TimeOnly.FromDateTime(now);
        }
        else if (record.EndWork == null)
        {
            record.EndWork = TimeOnly.FromDateTime(now);
        }
        else
        {
            return Result.Fail("All time records for today have already been completed.");
        }

        await _repository.SaveChangesAsync();
        return Result.Ok().WithSuccess("Time successfully recorded.");
    }
}
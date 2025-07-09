namespace Infrastructure.Repository;

public class DailyTimeRecordRepository : IDailyTimeRecordRepository
{
    private readonly AppDbContext _dbContext;

    public DailyTimeRecordRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> DailyTimeRecordExistsAsync(Guid userId, DateTime date)
    {
        var record = await _dbContext.TimeRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(tr => tr.EmployeeId == userId && tr.Date == DateOnly.FromDateTime(date));
        return record != null;   
    }

    public async Task<IEnumerable<DailyTimeRecord>> GetDailyRecordsForUserAsync(Guid userId)
    {
        return await _dbContext.TimeRecords
            .Where(tr => tr.EmployeeId == userId)
            .ToListAsync();
    }

    public async Task<TimeSpan> GetTotalTimeForUserAsync(Guid userId)
    {
        var today = DateTime.Now;
        var startOfMonth = new DateOnly(today.Year, today.Month, 1);

        var records = await _dbContext.TimeRecords
            .Where(tr => tr.EmployeeId == userId
                        && tr.Date >= startOfMonth
                        && tr.StartWork.HasValue
                        && tr.EndWork.HasValue)
            .ToListAsync();

        TimeSpan totalWorked = TimeSpan.Zero;

        foreach (var record in records)
        {
            var workDuration = record.EndWork!.Value - record.StartWork!.Value;
            var lunchDuration = (record.StartLunch.HasValue && record.EndLunch.HasValue)
                ? record.EndLunch.Value - record.StartLunch.Value
                : TimeSpan.Zero;

            totalWorked += (workDuration - lunchDuration);
        }

        return totalWorked;
    }

    public Task RegisterDailyTimeRecordAsync(Guid userId, DateTime date, TimeSpan time)
    {
        var record = new DailyTimeRecord
        {
            Id = Guid.NewGuid(),
            EmployeeId = userId,
            Date = DateOnly.FromDateTime(date),
            StartWork = TimeOnly.FromDateTime(date.Date.Add(time)),
            StartLunch = null,
            EndLunch = null,
            EndWork = null
        };

        _dbContext.TimeRecords.Add(record);
        return _dbContext.SaveChangesAsync();
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}

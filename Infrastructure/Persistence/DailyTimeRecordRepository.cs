using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

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
            .FirstOrDefaultAsync(tr => tr.EmployeeId == userId && tr.Date == DateOnly.FromDateTime(date));
        return record != null;   
    }

    public async Task<IEnumerable<DailyTimeRecord>> GetDailyRecordsForUserAsync(Guid userId)
    {
        return await _dbContext.TimeRecords
            .Where(tr => tr.EmployeeId == userId)
            .Select(tr => new DailyTimeRecord
            {
                Id = tr.Id,
                EmployeeId = tr.EmployeeId,
                Date = tr.Date,
                StartWork = tr.StartWork,
                StartLunch = tr.StartLunch,
                EndLunch = tr.EndLunch,
                EndWork = tr.EndWork
            })
            .ToListAsync();
    }

    public Task<TimeSpan> GetTotalTimeForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
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

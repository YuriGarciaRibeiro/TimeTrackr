using Core.Entities;

namespace Core.Interfaces;

public interface IDailyTimeRecordRepository
{
    Task RegisterDailyTimeRecordAsync(Guid userId, DateTime date, TimeSpan time);
    Task<bool> DailyTimeRecordExistsAsync(Guid userId, DateTime date);
    Task<TimeSpan> GetTotalTimeForUserAsync(Guid userId);
    Task<IEnumerable<DailyTimeRecord>> GetDailyRecordsForUserAsync(Guid userId);
    Task SaveChangesAsync();
}

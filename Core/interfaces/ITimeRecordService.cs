using FluentResults;

namespace Core.Interfaces;

public interface ITimeRecordService
{
    Task<Result> RegisterTimeAsync(Guid userId, DateTime now);
}

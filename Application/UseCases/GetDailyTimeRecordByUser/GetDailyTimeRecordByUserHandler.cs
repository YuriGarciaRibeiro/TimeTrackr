
namespace Application.UseCases.GetDailyTimeRecordByUser;

public class GetDailyTimeRecordByUserHandler
    (IDailyTimeRecordRepository repository)
    : IQueryHandler<GetDailyTimeRecordByUserQuery, GetDailyTimeRecordByUserResponse>
{
    public async Task<Result<GetDailyTimeRecordByUserResponse>> Handle(GetDailyTimeRecordByUserQuery request, CancellationToken cancellationToken)
    {
        var records = await repository.GetDailyRecordsForUserAsync(request.UserId);

        if (records == null || !records.Any())
            return Result.Fail("No time records found for the specified user.");

        var response = new GetDailyTimeRecordByUserResponse
        {
            UserId = request.UserId,
            TimeRecords = records.Select(r => new TimeRecordResponse
            {
                Id = r.Id,
                Date = r.Date,
                StartWork = r.StartWork,
                StartLunch = r.StartLunch,
                EndLunch = r.EndLunch,
                EndWork = r.EndWork
            }).ToList()
        };

        return Result.Ok(response);
    }
}
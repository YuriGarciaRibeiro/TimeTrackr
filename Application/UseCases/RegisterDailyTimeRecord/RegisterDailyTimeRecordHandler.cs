namespace Application.UseCases.RegisterDailyTimeRecord;

public class RegisterDailyTimeRecordHandler
    (ITimeRecordService timeRecordService)
    : ICommandHandler<RegisterDailyTimeRecordCommand, RegisterDailyTimeRecordResponse>
{
    public async Task<Result<RegisterDailyTimeRecordResponse>> Handle(RegisterDailyTimeRecordCommand request, CancellationToken cancellationToken)
    {
        var result = await timeRecordService.RegisterTimeAsync(request.UserId, request.Date ?? DateTime.Now);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        return Result.Ok(new RegisterDailyTimeRecordResponse { Id = Guid.NewGuid() });
    }
}


namespace Application.UseCases.GetDailyTimeRecordByUser;

public class GetDailyTimeRecordByUserQuery : IQuery<GetDailyTimeRecordByUserResponse>
{
    public Guid UserId { get; set; }
}

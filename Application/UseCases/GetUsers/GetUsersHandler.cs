using Core.Interfaces;

namespace Application.UseCases.GetUsers;

public class GetUsersHandler
    (IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, GetUsersResponse>
{
    public async Task<Result<GetUsersResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync();
        if (users.IsFailed)
            return Result.Fail(users.Errors);

        var response = new GetUsersResponse
        {
            Users = users.Value.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                DailyTimeRecords = user.TimeRecords.Select(record => new DailyTimeRecordDto
                {
                    Id = record.Id,
                    Date = record.Date,
                    StartWork = record.StartWork,
                    StartLunch = record.StartLunch,
                    EndLunch = record.EndLunch,
                    EndWork = record.EndWork
                }).ToList()
            }).ToList()
        };

        return Result.Ok(response);
    }
}

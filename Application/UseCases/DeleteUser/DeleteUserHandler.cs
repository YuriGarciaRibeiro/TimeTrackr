namespace Application.UseCases.DeleteUser;

public class DeleteUserHandler
    (IUserRepository userRepository)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user.IsFailed)
            return Result.Fail(user.Errors);

        var deleteResult = await userRepository.DeleteAsync(user.Value.Id);
        if (deleteResult.IsFailed)
            return Result.Fail(deleteResult.Errors);

        return Result.Ok();
    }
}

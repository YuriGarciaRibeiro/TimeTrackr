namespace Application.UseCases.DeleteUser;

public class DeleteUserCommand : ICommand
{
    public Guid UserId { get; set; }   
}

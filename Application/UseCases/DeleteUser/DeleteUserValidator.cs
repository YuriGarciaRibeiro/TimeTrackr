using FluentValidation;

namespace Application.UseCases.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("User ID must not be empty.")
            .NotNull().WithMessage("User ID must not be null.")
            .Must(id => id != Guid.Empty).WithMessage("User ID must be a valid GUID.");
    }
}

using FluentValidation;

namespace Application.UseCases.RegisterDailyTimeRecord;

public class RegisterDailyTimeRecordValidator : AbstractValidator<RegisterDailyTimeRecordCommand>
{
    public RegisterDailyTimeRecordValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");
    }
}

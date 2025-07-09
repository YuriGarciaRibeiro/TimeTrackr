namespace Application.UseCases.RegisterDailyTimeRecord;

public class RegisterDailyTimeRecordValidator : AbstractValidator<RegisterDailyTimeRecordCommand>
{
    public RegisterDailyTimeRecordValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}

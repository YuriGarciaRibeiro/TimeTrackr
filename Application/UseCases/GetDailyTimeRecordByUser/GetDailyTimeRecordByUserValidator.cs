namespace Application.UseCases.GetDailyTimeRecordByUser;

public class GetDailyTimeRecordByUserValidator : AbstractValidator<GetDailyTimeRecordByUserQuery>
{
    public GetDailyTimeRecordByUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty.")
            .Must(BeAValidGuid).WithMessage("User ID must be a valid GUID.");
    }

    private bool BeAValidGuid(Guid userId)
    {
        return userId != Guid.Empty;
    }
}

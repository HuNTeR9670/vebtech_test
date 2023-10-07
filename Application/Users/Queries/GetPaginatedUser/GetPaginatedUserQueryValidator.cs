namespace VebtechTest.Application.Users.Queries.GetPaginatedUser;

public class GetPaginatedUserQueryValidator : AbstractValidator<GetPaginatedUserQuery>
{
    public GetPaginatedUserQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(v => v.Age)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Age at least greater than or equal to 1.")
            .When(v => v.Age.HasValue);
    }
}

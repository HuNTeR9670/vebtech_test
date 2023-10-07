namespace VebtechTest.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandValidator(IApplicationDbContext context) 
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(v => v.Age)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Age at least greater than or equal to 1.");

        RuleFor(v => v.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is incorrect")
            .MustAsync(IsEmailValidAsync)
            .WithMessage("Email must be unique");
    }

    private async Task<bool> IsEmailValidAsync(string? email, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(email))
            return false;

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user == null;
    }
}

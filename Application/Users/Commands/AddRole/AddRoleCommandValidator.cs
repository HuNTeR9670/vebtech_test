namespace VebtechTest.Application.Users.Commands.AddRole;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRoleCommandValidator(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        RuleFor(v => v.Roles)
            .NotEmpty()
            .NotNull()
            .WithMessage("Roles must be exitst")
            .MustAsync(IsRolesValidAsync)
            .WithMessage("Unknown role value");
    }

    private async Task<bool> IsRolesValidAsync(string[]? roles, CancellationToken cancellationToken)
    {
        if (roles == null)
            return false;

        var exitsRoles = await _context.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var lowerRoles = roles.Select(r => r.ToLowerInvariant());       

        return exitsRoles.Where(r => lowerRoles.Contains(r.Name.ToLowerInvariant())).Count() != 0;
    }

}

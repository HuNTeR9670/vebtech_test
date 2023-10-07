using VebtechTest.Application.Common.Exceptions;

namespace VebtechTest.Application.Users.Commands.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .SingleOrDefaultAsync(u => u.Id.ToString() == request.UserId, cancellationToken)
            ?? throw new NotFoundException("User", request.UserId ?? string.Empty);


        var roles = request.Roles!.Select(r => r.ToLowerInvariant());
        var userRoles = user.Roles!.Select(r => r.Name.ToLowerInvariant());

        var allRoles = await _context.Roles.ToListAsync(cancellationToken);
        var addedRoles = roles.Except(userRoles);

        var newRoles = allRoles.Where(r => addedRoles.Contains(r.Name.ToLowerInvariant())).ToList();

        foreach (var role in newRoles)
        {
            user.Roles.Add(role);
        }

       await _context.SaveChangesAsync(cancellationToken);
    }
}

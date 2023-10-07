namespace VebtechTest.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<string> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Id.ToString().ToLower() == command.Id!.ToLower(), cancellationToken)
            ?? throw new NotFoundException("User", command.Id ?? string.Empty);

        user.Name = command.Name!;
        user.Age = command.Age;
        user.Email = command.Email!;

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id.ToString();
    }
}

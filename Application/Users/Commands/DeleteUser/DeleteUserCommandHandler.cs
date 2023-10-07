using VebtechTest.Application.Common.Exceptions;

namespace VebtechTest.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    { 
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users    
            .SingleOrDefaultAsync(u => u.Id.ToString().ToLower() == command.Id!.ToLower(), cancellationToken)
            ?? throw new NotFoundException("User", command.Id ?? string.Empty);

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

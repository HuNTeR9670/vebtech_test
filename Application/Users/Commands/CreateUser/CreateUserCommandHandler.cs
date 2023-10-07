namespace VebtechTest.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<string> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = command.Name!,
            Age = command.Age,
            Email = command.Email!
        };

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id.ToString();
    }
}

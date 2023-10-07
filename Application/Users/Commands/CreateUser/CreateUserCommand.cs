namespace VebtechTest.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public string? Name { get; init; }
    public int Age { get; init; }
    public string? Email { get; init; }
}

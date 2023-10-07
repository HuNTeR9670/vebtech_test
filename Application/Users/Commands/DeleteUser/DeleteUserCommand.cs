namespace VebtechTest.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand : IRequest
{
    public string? Id { get; init; }
}

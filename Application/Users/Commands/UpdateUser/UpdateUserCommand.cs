using VebtechTest.Application.Users.Commands.CreateUser;

namespace VebtechTest.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : CreateUserCommand
{
    public string? Id { get; init; }
}

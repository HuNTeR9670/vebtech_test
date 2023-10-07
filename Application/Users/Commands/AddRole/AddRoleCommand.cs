namespace VebtechTest.Application.Users.Commands.AddRole;

public record AddRoleCommand : IRequest
{
    public string? UserId { get; init; }
    public string[]? Roles { get; init; }
}

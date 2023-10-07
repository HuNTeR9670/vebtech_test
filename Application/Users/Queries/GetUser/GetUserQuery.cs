namespace VebtechTest.Application.Users.Queries.GetUser;

public record GetUserQuery : IRequest<UserDto>
{
    public string? Id { get; init; }
}

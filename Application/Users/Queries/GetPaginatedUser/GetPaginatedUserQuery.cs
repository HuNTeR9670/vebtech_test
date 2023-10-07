namespace VebtechTest.Application.Users.Queries.GetPaginatedUser;

public record GetPaginatedUserQuery : BasePaginatedQuery<UserItemDto>
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public int? Age { get; init; }
    public string? Email { get; init; }
    public string? RoleName { get; init; }
}

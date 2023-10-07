namespace VebtechTest.Application.Users.Queries.GetPaginatedUser;

public record UserItemDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Email { get; init; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserItemDto>();
        }
    }
}

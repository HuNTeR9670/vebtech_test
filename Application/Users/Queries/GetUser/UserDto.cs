namespace VebtechTest.Application.Users.Queries.GetUser;

public record UserDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Email { get; init; }
    public required string[] Roles { get; init; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, opt => opt.MapFrom(x => x.Roles.Select(r => r.Name)));
        }
    }
}

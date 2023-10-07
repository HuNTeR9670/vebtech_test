namespace VebtechTest.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id.ToString().ToLower() == request.Id!.ToLower(), cancellationToken)
            ?? throw new NotFoundException("User", request.Id ?? string.Empty);

        return _mapper.Map<UserDto>(user);
    }
}

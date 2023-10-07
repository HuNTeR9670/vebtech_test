namespace VebtechTest.Application.Users.Queries.GetPaginatedUser;

public class GetPaginatedUserQueryHandler : IRequestHandler<GetPaginatedUserQuery, PaginatedList<UserItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaginatedUserQueryHandler(
        IApplicationDbContext context, 
        IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaginatedList<UserItemDto>> Handle(GetPaginatedUserQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(request.Id))
            usersQuery = usersQuery.Where(u => u.Id.ToString().ToLower() == request.Id.ToLower());

        if (!string.IsNullOrEmpty(request.Name))
            usersQuery = usersQuery.Where(u => u.Name.ToLower().Contains(request.Name.ToLower()));

        if (request.Age.HasValue)
            usersQuery = usersQuery.Where(u => u.Age == request.Age.Value);

        if (!string.IsNullOrEmpty(request.Email))
            usersQuery = usersQuery.Where(u => u.Email.ToLower() == request.Email.ToLower());

        if (!string.IsNullOrEmpty(request.RoleName))
            usersQuery = usersQuery.Where(u => u.Roles!
            .Any(r => r.Name.ToLower() == request.RoleName.ToLower()));

        if (request.SortField.ToLower() == "rolename" && request.SortOrder == SortingDirection.Ascending)
            usersQuery = usersQuery.OrderBy(u => u.Roles!.First().Name);
        else if (request.SortField.ToLower() == "rolename" && request.SortOrder == SortingDirection.Descending)
            usersQuery = usersQuery.OrderByDescending(u => u.Roles!.First().Name);
        else
            usersQuery = usersQuery.OrderByProperty(request.SortField, request.SortOrder);

        return await usersQuery
               .ProjectTo<UserItemDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

    }
}

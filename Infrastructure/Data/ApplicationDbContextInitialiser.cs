using Microsoft.Extensions.Logging;
using VebtechTest.Domain.Constants;

namespace VebtechTest.Infrastructure.Data;


public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var superAdminRole = new Role
        {
            Name = Roles.SuperAdmin
        };

        if (!_context.Roles.Any())
        {
            var roles = Roles.AllRoles.Select(r => new Role { Name = r });
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        var superAdmin = await _context.Roles
            .Where(r => r.Name == Roles.SuperAdmin)
            .ToListAsync();

        // Default users
        var administrator = new User 
        { 
            Name = "administrator@localhost", 
            Email = "administrator@localhost", 
            Age = 30,
            Roles = superAdmin,
        };

        if (_context.Users.All(u => u.Email != administrator.Email))
        {
            await _context.Users.AddAsync(administrator);
            await _context.SaveChangesAsync();
        }
    }
}

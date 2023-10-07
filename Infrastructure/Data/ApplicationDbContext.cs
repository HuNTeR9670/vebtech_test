namespace VebtechTest.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //builder.Entity<User>()
        //    .HasMany(p => p.Roles)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(builder);
    }
}

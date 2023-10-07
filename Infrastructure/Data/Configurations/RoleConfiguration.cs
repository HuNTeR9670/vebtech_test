namespace VebtechTest.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(b => b.Name)
               .HasMaxLength(256);

        builder.HasIndex(b => b.Name);
    }
}

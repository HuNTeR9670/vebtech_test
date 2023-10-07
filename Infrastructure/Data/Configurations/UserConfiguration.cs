namespace VebtechTest.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(b => b.Name)
               .HasMaxLength(256);
        
        builder.Property(b => b.Email)
                .HasMaxLength(256);

        builder.HasIndex(b => b.Name);
        builder.HasIndex(b => b.Email);
    }
}

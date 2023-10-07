namespace VebtechTest.Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required int Age { get; set; }
    public required string Email { get; set; }
    public ICollection<Role>? Roles { get; set; }
}

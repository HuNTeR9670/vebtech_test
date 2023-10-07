namespace VebtechTest.Domain.Constants;

public static class Roles
{
    public const string User = nameof(User);
    public const string Admin = nameof(Admin);
    public const string Support = nameof(Support);
    public const string SuperAdmin = nameof(SuperAdmin);

    public static IEnumerable<string> AllRoles
    {
        get
        {
            yield return User;
            yield return Admin;
            yield return Support;
            yield return SuperAdmin;
        }
    }
}

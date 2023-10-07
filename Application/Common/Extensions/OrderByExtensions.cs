namespace VebtechTest.Application.Common.Extensions;

public static class OrderByExtensions
{
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, SortingDirection direction)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            return source;


        return direction == SortingDirection.Descending ? source.OrderByDescending(x => EF.Property<T>(x!, property.Name))
            : source.OrderBy(x => EF.Property<T>(x!, property.Name));
    }
}


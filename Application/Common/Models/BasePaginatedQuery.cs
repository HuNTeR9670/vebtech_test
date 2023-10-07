namespace VebtechTest.Application.Common.Models;

public enum SortingDirection
{
    Ascending = 0,
    Descending,
}


public record BasePaginatedQuery<T> : IRequest<PaginatedList<T>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 25;
    public string SortField { get; init; } = "Id";
    public SortingDirection SortOrder { get; init; } = SortingDirection.Descending;
}


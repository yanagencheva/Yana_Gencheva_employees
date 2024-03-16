namespace EmployeesAPI.Common.Models.Response;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public Pagination Pagination { get; set; }
    public bool HasMore { get; set; }

    public PaginatedResult()
    {
        Items = Array.Empty<T>();
        Pagination = new Pagination();
    }

    public PaginatedResult(IEnumerable<T> items, Pagination pagination)
    {
        Pagination = pagination;
        HasMore = items.Count() > pagination.Offset;
        Items = items.Take(pagination.Offset);
    }
}

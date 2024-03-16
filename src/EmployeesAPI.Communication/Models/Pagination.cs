namespace EmployeesAPI.Common.Models;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int Offset { get; set; } = 20;
    public bool HasMore { get; set; } = false;
    public string SortBy { get; set; } = string.Empty;
    public bool Ascending { get; set; } = true;
}

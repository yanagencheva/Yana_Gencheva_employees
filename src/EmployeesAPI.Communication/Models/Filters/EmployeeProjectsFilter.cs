namespace EmployeesAPI.Common.Models.Filters;

public class EmployeeProjectsFilter : Pagination
{
	public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}

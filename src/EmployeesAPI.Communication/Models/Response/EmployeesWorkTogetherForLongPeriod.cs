namespace EmployeesAPI.Common.Models.Response;

public record EmployeesWorkTogetherForLongPeriod
{
    public int FirstEmpID { get; set; }
    public int SecondEmpID { get; set; }
    public int ProjectID { get; set; }
    public TimeSpan TotalDays { get; set; }
}

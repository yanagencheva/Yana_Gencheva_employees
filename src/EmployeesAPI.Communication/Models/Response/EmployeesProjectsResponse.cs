namespace EmployeesAPI.Common.Models.Response;
public record EmployeesProjectsResponse
{
    public int FirstEmpID { get; set; }
    public int SecondEmpID { get; set; }
    public int ProjectID { get; set; }
    public double DaysWorked { get; set; }
}

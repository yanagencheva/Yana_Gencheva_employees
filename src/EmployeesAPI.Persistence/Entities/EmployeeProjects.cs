using EmployeesAPI.Persistence.Entities.Abstractions;

namespace EmployeesAPI.Persistence.Entities;

public class EmployeeProjects : Entity
{
    public int EmpID { get; set; }
    public int ProjectID { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
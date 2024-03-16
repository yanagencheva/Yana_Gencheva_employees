using EmployeesAPI.Common.Models.Response;
using EmployeesAPI.Persistence.Entities;

namespace EmployeesAPI.Persistence.Services.Abstractions;

public interface IEmployeeProjectsService
{
    IEnumerable<EmployeesWorkTogetherForLongPeriod> GetEmployeesWorkTogetherForLongPeriod();
    IEnumerable<EmployeesProjectsResponse> GetAll();
    EmployeeProjects CreateEmployeeProject(EmployeeProjects employeeProject);
}

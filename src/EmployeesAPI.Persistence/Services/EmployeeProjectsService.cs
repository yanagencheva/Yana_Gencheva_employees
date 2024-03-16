using EmployeesAPI.Common.Models.Response;
using EmployeesAPI.Persistence.Entities;
using EmployeesAPI.Persistence.Services.Abstractions;

namespace EmployeesAPI.Persistence.Services;

internal class EmployeeProjectsService : IEmployeeProjectsService
{
    private readonly IUnitOfWork unitOfWork;    


    public EmployeeProjectsService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public IEnumerable<EmployeesProjectsResponse> GetAll()
    {
        var employees = unitOfWork.EmployeeProjects.All();

        var crossJoinLambda = employees.SelectMany(t1 =>
        employees.Where(x => x.EmpID != t1.EmpID && x.ProjectID == t1.ProjectID)
        .Select(t2 => new EmployeesWorkTogetherForLongPeriod
        {
            ProjectID = t1.ProjectID,
            FirstEmpID = Math.Max(t1.EmpID, t2.EmpID),
            SecondEmpID = Math.Min(t1.EmpID, t2.EmpID),
            TotalDays = (new List<DateTime> { t1.DateTo, t2.DateTo }.Min() - new List<DateTime> { t1.DateFrom, t2.DateFrom }.Max())
        }));

        var employeeItems = crossJoinLambda.ToList()
                   .DistinctBy(x => new { x.FirstEmpID, x.SecondEmpID });

        var result = employeeItems.Select(x => new EmployeesProjectsResponse
        {
            FirstEmpID = x.FirstEmpID,
            SecondEmpID = x.SecondEmpID,
            ProjectID = x.ProjectID,
            DaysWorked = (x.TotalDays.TotalDays > 0 ? Math.Round(x.TotalDays.TotalDays,2) : 0)
        }).ToList();

        return result;
    }

    public EmployeeProjects CreateEmployeeProject(EmployeeProjects employeeProject)
    {
        var employeeItems = unitOfWork.EmployeeProjects.Add(employeeProject);
        unitOfWork.Commit();
        return employeeItems;
    
    }

    public IEnumerable<EmployeesWorkTogetherForLongPeriod> GetEmployeesWorkTogetherForLongPeriod()
    {
        var employees = unitOfWork.EmployeeProjects.All();

        var crossJoinLambda = employees.SelectMany(t1 => 
        employees.Where(x=>  x.EmpID != t1.EmpID && x.ProjectID == t1.ProjectID)
        .Select(t2 => new EmployeesWorkTogetherForLongPeriod
        {
            ProjectID = t1.ProjectID,
            FirstEmpID = Math.Max(t1.EmpID, t2.EmpID),
            SecondEmpID = Math.Min(t1.EmpID, t2.EmpID),
            TotalDays = (new List<DateTime> { t1.DateTo, t2.DateTo }.Min() - new List<DateTime> { t1.DateFrom, t2.DateFrom }.Max())
        }));

       
        var longestPeriod = crossJoinLambda.ToList().OrderByDescending(x => x.TotalDays).Take(1).FirstOrDefault();

        if (!(longestPeriod.TotalDays.TotalDays > 0)) return default;

        var employeesWorkedlongPer = crossJoinLambda.ToList()
                   .Where(x => x.TotalDays.TotalDays == longestPeriod.TotalDays.TotalDays)
                   .DistinctBy(x => new { x.FirstEmpID, x.SecondEmpID });
                 
        return employeesWorkedlongPer;
    }
}

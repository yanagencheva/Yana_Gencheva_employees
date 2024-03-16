using Microsoft.Extensions.Logging;
using EmployeesAPI.Persistence.Entities;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace EmployeesAPI.Persistence.Initializer.Steps;
internal class SeedInitializationData : StepBody
{        
    private readonly ILogger<SeedInitializationData> logger;    
    private readonly IDbContext dbContext;

    public SeedInitializationData(ILogger<SeedInitializationData> logger,
        IDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var empProj = new EmployeeProjects() {
            EmpID = 100,
            ProjectID = 1,
            DateFrom = DateTime.UtcNow,
            DateTo = DateTime.UtcNow
        };        
        dbContext.Set<EmployeeProjects>().Add(empProj);
        dbContext.SaveChanges();
        
        logger.LogInformation("inserted employee in project");

        return ExecutionResult.Next();
    }
}

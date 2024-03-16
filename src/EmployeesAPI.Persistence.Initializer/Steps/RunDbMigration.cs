using Microsoft.EntityFrameworkCore;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace EmployeesAPI.Persistence.Initializer.Steps;
internal class RunDbMigration : StepBody
{
    private readonly IDbContext dbContext;

    public RunDbMigration(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        dbContext.Database.Migrate();

        return ExecutionResult.Next();
    }
}

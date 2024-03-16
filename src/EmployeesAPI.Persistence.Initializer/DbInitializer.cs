using Microsoft.Extensions.Hosting;
using TBS.Toolkit.Common.Extensions.Initializer;

namespace EmployeesAPI.Persistence.Initializer;
internal class DbInitializer : BackgroundService
{
    private readonly IWorkflowRunner dbInitializerWorkflow;

    public DbInitializer(IWorkflowRunner dbInitializerWorkflow)
    {
        this.dbInitializerWorkflow = dbInitializerWorkflow;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        dbInitializerWorkflow.Run();
        await StopAsync(new CancellationToken(true));
    }
}
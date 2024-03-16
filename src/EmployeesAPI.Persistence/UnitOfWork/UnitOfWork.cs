using Microsoft.Extensions.Logging;
using EmployeesAPI.Persistence.Entities;

namespace EmployeesAPI.Persistence;
internal class UnitOfWork : IUnitOfWork
{
    private readonly IDbContext dbContext;
    private readonly ILogger<UnitOfWork> logger;

    public IRepository<EmployeeProjects> EmployeeProjects { get; private set; }

    public UnitOfWork(IDbContext dbContext,
        ILogger<UnitOfWork> logger,
        IRepository<EmployeeProjects> employeeProjects)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        EmployeeProjects = employeeProjects;
    }

    public void Commit()
    {
        try
        {
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error by saveChanges");
            Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        dbContext.Dispose();
    }
}

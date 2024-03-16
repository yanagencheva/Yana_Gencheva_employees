using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Persistence.Entities;
using EmployeesAPI.Persistence.EntitiesConfigurations;

namespace EmployeesAPI.Persistence;

public class EmployeeContext : DbContext, IDbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
    {
    }

    public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public DbSet<EmployeeProjects> EmployeeProjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyCommonTypeSettings();
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
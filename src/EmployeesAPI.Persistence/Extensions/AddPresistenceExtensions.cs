using EmployeesAPI.Persistence;
using EmployeesAPI.Persistence.Services;
using EmployeesAPI.Persistence.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeAPI.Persistence.Extensions;

public static class AddPresistenceExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDbContext, EmployeeContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString(nameof(EmployeeContext))));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IEmployeeProjectsService, EmployeeProjectsService>();

        services.AddScoped<IEmployeeProjectsService, EmployeeProjectsService>();
    }
}
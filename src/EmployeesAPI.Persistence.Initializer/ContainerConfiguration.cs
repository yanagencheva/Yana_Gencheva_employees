using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TBS.Toolkit.Common.Extensions.Initializer;
using EmployeesAPI.Persistence.Initializer.Steps;

namespace EmployeesAPI.Persistence.Initializer;
public static class ContainerConfiguration
{
    public static IServiceCollection Configurate(this IServiceCollection services, HostBuilderContext builderContext)
    {
        services.AddScoped<IDbContext, EmployeeContext>()
        .AddDbContext<EmployeeContext>(options => {
            options.UseNpgsql(
                builderContext.Configuration.GetConnectionString(nameof(EmployeeContext)),
                sqlOption => sqlOption.MigrationsAssembly(typeof(ContainerConfiguration).Assembly.FullName));
        })        
        .AddScoped<RunDbMigration>()
        .AddScoped<SeedInitializationData>()
        .AddWorkflow(builderContext);
        
        return services;
    }
}
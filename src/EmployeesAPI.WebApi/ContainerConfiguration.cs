using EmployeeAPI.Persistence.Extensions;

namespace EmployeesAPI.WebApi;

public static class ContainerConfiguration
{
    public static IServiceCollection Configurate(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);

        return services;
    }
}

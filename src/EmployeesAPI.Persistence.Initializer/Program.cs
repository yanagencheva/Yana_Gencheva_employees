using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeesAPI.Persistence.Initializer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, conf) =>
    {
        conf.SetBasePath($"{hostContext.HostingEnvironment.ContentRootPath}")
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
              .AddJsonFile("appsettings.Local.json", optional: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services
            .Configurate(hostContext)
            .AddHostedService<DbInitializer>();
    })
    .Build();

await host.RunAsync();

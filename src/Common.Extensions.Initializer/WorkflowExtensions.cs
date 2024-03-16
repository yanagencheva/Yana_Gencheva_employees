using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace TBS.Toolkit.Common.Extensions.Initializer
{
	public static class WorkflowExtensions
	{
        public static void AddWorkflow(this IServiceCollection services, HostBuilderContext builderContext)
        {
            services.Configure<WorkflowSettings>(options => builderContext.Configuration.GetSection(nameof(WorkflowSettings)).Bind(options));
            services.AddWorkflow();
            services.AddWorkflowDSL();
            services.AddSingleton<WorkflowCore.Interface.IWorkflowRegistry, WorkflowCore.Services.WorkflowRegistry>();
            services.AddSingleton<IWorkflowRunner, WorkflowRunner>();
        }
    }
}
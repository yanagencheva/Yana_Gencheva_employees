using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Models.LifeCycleEvents;
using WorkflowCore.Services.DefinitionStorage;

namespace TBS.Toolkit.Common.Extensions.Initializer
{
    public class WorkflowRunner : IWorkflowRunner
    {
        private readonly ILogger<WorkflowRunner> logger;
        private readonly IDefinitionLoader stepsLoader;
        private readonly IWorkflowHost workflowHost;
        private readonly WorkflowSettings settings;
        private WorkflowDefinition workflowDefinition;

        public WorkflowRunner(
            ILogger<WorkflowRunner> logger,
            IDefinitionLoader stepsLoader,
            IOptions<WorkflowSettings> options,
            IWorkflowHost workflowHost)
        {
            this.logger = logger;
            this.stepsLoader = stepsLoader;
            this.settings = options.Value;
            this.workflowHost = workflowHost;
        }

        public void Run()
        {
            logger.LogInformation($"Running Workflow {settings.WorkflowId}");

            var workflowDefinitionJson = File.ReadAllText(settings.WorkflowDefinitionPath);
            workflowDefinition = stepsLoader.LoadDefinition(workflowDefinitionJson, Deserializers.Json);
            workflowHost.Start();
            workflowHost.StartWorkflow(settings.WorkflowId, version: 1);
            workflowHost.OnStepError += WorkflowHost_OnStepError;
            workflowHost.OnLifeCycleEvent += WorkflowHost_OnLifeCycleEvent;
        }

        private void WorkflowHost_OnLifeCycleEvent(LifeCycleEvent lifeCycleEvent)
        {
            if (lifeCycleEvent is StepStarted stepStarted)
            {
                logger.LogInformation($"Started Step: {workflowDefinition.Steps.Single(x => x.Id == stepStarted.StepId).BodyType.Name}");
                return;
            }
            if (lifeCycleEvent is StepCompleted stepCompleated)
            {
                logger.LogInformation($"Completed Step: {workflowDefinition.Steps.Single(x => x.Id == stepCompleated.StepId).BodyType.Name}");
                return;
            }
            if (lifeCycleEvent is WorkflowCompleted workflowCompleted)
            {
                logger.LogInformation($"Completed Workflow: {workflowCompleted.WorkflowDefinitionId}");

                workflowHost.Stop();
                return;
            }
        }

        private void WorkflowHost_OnStepError(WorkflowInstance workflow, WorkflowStep step, Exception exception)
        {
            logger.LogError($"Step: {step.Name} Error: {exception.Message}", exception);
            workflowHost.Stop();
        }
    }
}

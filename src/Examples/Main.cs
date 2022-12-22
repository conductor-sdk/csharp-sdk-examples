using Conductor.Definition;
using Conductor.Definition.TaskType;
using Conductor.Executor;
using Microsoft.Extensions.Hosting;
using Examples.Api;
using Examples.Worker;

ConductorWorkflow workflow = new ConductorWorkflow()
        .WithName("csharp-sdk-example-workflow")
        .WithVersion(1)
        .WithTask(new SimpleTask("csharp-sdk-example-task", "csharp-sdk-example-task"));

WorkflowExecutor workflowExecutor = ApiUtil.GetWorkflowExecutor();
workflowExecutor.RegisterWorkflow(workflow, true);

IHost host = WorkerUtil.GetWorkerHost();
host.RunAsync();

// List<String> workflowIds = StartWorkflows(workflow);
// Thread.Sleep(WORKFLOW_EXECUTION_TIMEOUT_SECONDS * 1000);

// foreach (string workflowId in workflowIds)
// {
//     ValidateWorkflowCompletion(workflowId);
// }

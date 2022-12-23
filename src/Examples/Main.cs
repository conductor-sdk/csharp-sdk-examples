using Conductor.Api;
using Conductor.Definition;
using Conductor.Definition.TaskType;
using Conductor.Executor;
using Conductor.Client.Models;
using Microsoft.Extensions.Hosting;
using Examples.Api;
using Examples.Worker;
using System.Collections.Generic;
using System.Threading;
using System;

static List<string> StartWorkflows(ConductorWorkflow workflow, int workflowQuantity, WorkflowExecutor workflowExecutor)
{
    List<string> workflowIds = new List<string>();
    for (int i = 0; i < workflowQuantity; i += 1)
    {
        string workflowId = workflowExecutor.StartWorkflow(workflow);
        workflowIds.Add(workflowId);
    }
    return workflowIds;
}

static bool IsWorkflowCompleted(string workflowId)
{
    WorkflowStatus workflowStatus = ApiUtil.GetClient<WorkflowResourceApi>()
        .GetWorkflowStatusSummary(workflowId);
    return workflowStatus.Status == WorkflowStatus.StatusEnum.COMPLETED;
}

ConductorWorkflow workflow = new ConductorWorkflow()
        .WithName("csharp-sdk-example-workflow")
        .WithVersion(1)
        .WithTask(
                new SimpleTask("csharp-sdk-example-task", "csharp-sdk-example-task")
        );

WorkflowExecutor workflowExecutor = ApiUtil.GetWorkflowExecutor();
workflowExecutor.RegisterWorkflow(workflow, true);

IHost host = WorkerUtil.GetWorkerHost();
host.RunAsync();

List<string> workflowIds = StartWorkflows(workflow, 10, workflowExecutor);
Thread.Sleep(15 * 1000);
workflowIds.ForEach(
        (workflowId) =>
        {
            bool isWorkflowCompleted = IsWorkflowCompleted(workflowId);
            Console.WriteLine(
                    $"workflowId: {workflowId}, completed: {isWorkflowCompleted}"
            );
        }
);

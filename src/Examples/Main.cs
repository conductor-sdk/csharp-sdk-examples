using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using System;
using System.Threading;

Examples.Worker.WorkerUtil.StartWorkers();
StartWorkflowAsync();

static void StartWorkflowAsync()
{
    ConductorWorkflow workflow = Examples.Workflow.WorkflowCreator.CreateAndRegisterWorkflow();
    StartWorkflowRequest startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);

    WorkflowResourceApi workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
    string workflowId = workflowClient.StartWorkflow(startWorkflowRequest);

    Thread.Sleep(6 * 1000);

    Console.WriteLine();
    Console.WriteLine("=======================================================================================");
    Console.WriteLine("Workflow Execution Completed");
    Console.WriteLine("Workflow Id: " + workflowId);
    Console.WriteLine("Workflow Execution Flow UI: " + Examples.Api.ApiUtil.GetWorkflowExecutionURL(workflowId));
    Console.WriteLine("=======================================================================================");

    Workflow receivedWorkflow = workflowClient.GetExecutionStatus(workflowId);
    if (receivedWorkflow.Status != Workflow.StatusEnum.COMPLETED)
    {
        throw new Exception($"workflow not completed, workflowId: {workflowId}");
    }
}
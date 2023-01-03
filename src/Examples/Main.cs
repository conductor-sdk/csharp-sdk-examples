using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using System;
using System.Threading;

ConductorWorkflow workflow = Examples.Workflow.WorkflowCreator.CreateAndRegisterWorkflow();
Examples.Worker.WorkerUtil.StartWorkers();
StartWorkflowsAsync(workflow);

static void StartWorkflowsAsync(ConductorWorkflow workflow)
{
    WorkflowResourceApi workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
    StartWorkflowRequest startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);
    string workflowId = workflowClient.StartWorkflow(startWorkflowRequest);
    Thread.Sleep(5 * 1000);
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
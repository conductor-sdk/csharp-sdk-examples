using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using System;
using System.Threading;

Examples.Worker.WorkerUtil.StartWorkers();

ConductorWorkflow workflow = Examples.Workflow.WorkflowCreator.CreateAndRegisterWorkflow();

StartWorkflowSync(workflow);
StartWorkflowAsync(workflow);

static void StartWorkflowSync(ConductorWorkflow workflow)
{
    StartWorkflowRequest startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);

    WorkflowResourceApi workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
    WorkflowRun workflowRun = workflowClient.ExecuteWorkflow(
        body: startWorkflowRequest,
        requestId: Guid.NewGuid().ToString(),
        name: workflow.Name,
        version: 1
    );

    Console.WriteLine();
    Console.WriteLine("=======================================================================================");
    Console.WriteLine("Workflow Execution Completed");
    Console.WriteLine("Workflow Id: " + workflowRun.WorkflowId);
    Console.WriteLine("Workflow Status: " + workflowRun.Status.ToString());
    Console.WriteLine("Workflow Output: " + workflowRun.Output.ToString());
    Console.WriteLine($"Workflow Execution Flow UI: {Examples.Api.ApiUtil.GetWorkflowExecutionURL(workflowRun.WorkflowId)}");
    Console.WriteLine("=======================================================================================");
}

static void StartWorkflowAsync(ConductorWorkflow workflow)
{
    StartWorkflowRequest startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);

    WorkflowResourceApi workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
    string workflowId = workflowClient.StartWorkflow(startWorkflowRequest);

    Thread.Sleep(7 * 1000);

    Console.WriteLine();
    Console.WriteLine("=======================================================================================");
    Console.WriteLine("Workflow Execution Completed");
    Console.WriteLine($"Workflow Id: {workflowId}");
    Console.WriteLine($"Workflow Execution Flow UI: {Examples.Api.ApiUtil.GetWorkflowExecutionURL(workflowId)}");
    Console.WriteLine("=======================================================================================");

    Workflow receivedWorkflow = workflowClient.GetExecutionStatus(workflowId);
    if (receivedWorkflow.Status != Workflow.StatusEnum.COMPLETED)
    {
        throw new Exception($"workflow not completed, workflowId: {workflowId}");
    }
}
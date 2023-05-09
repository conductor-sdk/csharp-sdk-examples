using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using Conductor.Client.Extensions;
using System;
using System.Threading;

var host = WorkflowTaskHost.CreateWorkerHost(
    ApiExtensions.GetConfiguration(),
    Microsoft.Extensions.Logging.LogLevel.Debug,
    workers: new Examples.Worker.GetUserInfo()
);

static void StartWorkflowSync(ConductorWorkflow workflow)
{
    var workflowClient = ApiExtensions.GetClient<WorkflowResourceApi>();
    var startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);

    var workflowRun = workflowClient.ExecuteWorkflow(
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
    var workflowClient = ApiExtensions.GetClient<WorkflowResourceApi>();

    var startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);
    var workflowId = workflowClient.StartWorkflow(startWorkflowRequest);

    Thread.Sleep(7 * 1000);

    Console.WriteLine();
    Console.WriteLine("=======================================================================================");
    Console.WriteLine("Workflow Execution Completed");
    Console.WriteLine($"Workflow Id: {workflowId}");
    Console.WriteLine($"Workflow Execution Flow UI: {Examples.Api.ApiUtil.GetWorkflowExecutionURL(workflowId)}");
    Console.WriteLine("=======================================================================================");

    var receivedWorkflow = workflowClient.GetExecutionStatus(workflowId);
    if (receivedWorkflow.Status != Workflow.StatusEnum.COMPLETED)
    {
        throw new Exception($"workflow not completed, workflowId: {workflowId}");
    }
}

var workflow = Examples.Workflow.WorkflowCreator.CreateAndRegisterWorkflow();
await host.StartAsync();
StartWorkflowSync(workflow);
StartWorkflowAsync(workflow);
await host.StopAsync();

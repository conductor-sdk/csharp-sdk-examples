using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using System;
using System.Threading;

ConductorWorkflow workflow = Examples.Workflow.WorkflowCreator.CreateAndRegisterWorkflow();
Examples.Worker.WorkerUtil.StartWorkers();

WorkflowResourceApi workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
StartWorkflowRequest startWorkflowRequest = Examples.Workflow.WorkflowCreator.GetStartWorkflowRequest(workflow);
string workflowId = workflowClient.StartWorkflow(startWorkflowRequest);

Console.WriteLine();
Console.WriteLine("=======================================================================================");
Console.WriteLine("Workflow Execution Completed");
Console.WriteLine($"Workflow Id: {workflowId}");

Thread.Sleep(4 * 1000);
Workflow receivedWorkflow = workflowClient.GetExecutionStatus(workflowId);

Console.WriteLine($"Workflow Status: {receivedWorkflow.Status.ToString()}");
Console.WriteLine($"Workflow Output: {receivedWorkflow.Output}");
Console.WriteLine(
    $"Workflow Execution Flow UI: {Examples.Api.ApiUtil.GetWorkflowExecutionURL(workflowId)}"
);
Console.WriteLine("=======================================================================================");

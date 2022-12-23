using Conductor.Api;
using Conductor.Client.Models;
using Conductor.Definition;
using Conductor.Definition.TaskType;
using Conductor.Executor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Examples.Workflow
{
    public class WorkflowUtil
    {
        public const string WORKFLOW_NAME = "example-csharp-workflow";
        public const string TASK_NAME = "example-csharp-simple-task";
        private static WorkflowExecutor _workflowExecutor;

        static WorkflowUtil()
        {
            _workflowExecutor = Examples.Api.ApiUtil.GetWorkflowExecutor();
        }

        public static void StartWorkflowsAndValidateTheirCompletion()
        {
            ConductorWorkflow exampleWorkflow = CreateExampleWorkflow();
            RegisterWorkflow(exampleWorkflow);
            List<string> workflowIds = StartWorkflows(exampleWorkflow, 3);
            Examples.Worker.WorkerUtil.StartWorkers();
            Thread.Sleep(15 * 1000);
            workflowIds.ForEach(
                    (workflowId) =>
                    {
                        AssertWorkflowCompletion(workflowId);
                    }
            );
        }

        public static void RegisterWorkflow(ConductorWorkflow workflow)
        {
            _workflowExecutor.RegisterWorkflow(workflow, true);
        }

        private static List<string> StartWorkflows(ConductorWorkflow workflow, int workflowQuantity)
        {
            List<string> workflowIds = new List<string>();
            for (int i = 0; i < workflowQuantity; i += 1)
            {
                string workflowId = _workflowExecutor.StartWorkflow(workflow);
                Console.WriteLine($"Started workflow with name {workflow.Name}, workflowId: {workflowId}");
                workflowIds.Add(workflowId);
            }
            return workflowIds;
        }

        private static void AssertWorkflowCompletion(string workflowId)
        {
            WorkflowStatus workflowStatus = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>()
                .GetWorkflowStatusSummary(workflowId);
            Console.WriteLine($"Asserting workflow completion, workflowId: {workflowId}, status: {workflowStatus.Status}");
            Debug.Assert(
                workflowStatus.Status.Equals(WorkflowStatus.StatusEnum.COMPLETED)
            );
        }

        private static ConductorWorkflow CreateExampleWorkflow()
        {
            return new ConductorWorkflow()
                .WithName(WORKFLOW_NAME)
                .WithVersion(1)
                .WithTask(new SimpleTask(TASK_NAME, TASK_NAME));
        }
    }
}
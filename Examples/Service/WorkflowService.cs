using Conductor.Api;
using Conductor.Client.Extensions;
using Conductor.Client.Models;
using Examples.Models;
using System;

namespace Examples.Service
{
    public class WorkflowService
    {
        private static readonly WorkflowResourceApi _workflowClient;
        private static readonly string WORKFLOW_NAME = "deposit_payment";
        private static readonly int WORKFLOW_VERSION = 1;

        static WorkflowService()
        {
            _workflowClient = ApiExtensions.GetClient<WorkflowResourceApi>();
        }

        public string StartDepositWorkflow(DepositDetail depositDetail)
        {
            // docs-marker-start-1
            var request = new StartWorkflowRequest
            {
                Name = WORKFLOW_NAME,
                Version = WORKFLOW_VERSION,
                Input = Examples.Util.TypeUtil.GetDictionaryFromObject(depositDetail)
            };
            var workflowId = _workflowClient.StartWorkflow(request);
            Console.WriteLine($"Started deposit workflow id: {workflowId}");
            return workflowId;
            // docs-marker-end-1
        }
    }
}

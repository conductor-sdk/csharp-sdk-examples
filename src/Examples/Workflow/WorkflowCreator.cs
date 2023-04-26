using Conductor.Api;
using Conductor.Definition;
using Conductor.Definition.TaskType;
using System.Collections.Generic;

namespace Examples.Workflow
{
    public class WorkflowCreator
    {
        private static WorkflowResourceApi _workflowClient;
        private static MetadataResourceApi _metadataClient;

        static WorkflowCreator()
        {
            _workflowClient = Examples.Api.ApiUtil.GetClient<WorkflowResourceApi>();
            _metadataClient = Examples.Api.ApiUtil.GetClient<MetadataResourceApi>();
        }

        public static ConductorWorkflow CreateAndRegisterWorkflow()
        {
            var workflow = Examples.Workflow.WorkflowCreator.CreateComplexWorkflow();
            _metadataClient.RegisterTaskDef(GetTaskDefs(workflow));
            _metadataClient.Create(
                body: workflow,
                overwrite: true
            );
            return workflow;
        }

        public static Conductor.Client.Models.StartWorkflowRequest GetStartWorkflowRequest(ConductorWorkflow workflow)
        {
            var workflowInput = new Examples.Workflow.WorkflowInput("userA");
            var startWorkflowRequest = workflow.GetStartWorkflowRequest();
            startWorkflowRequest.Input = new Dictionary<string, object>()
            {
                {"userId", workflowInput.UserId},
                {"notificationPref", workflowInput.NotificationPref.ToString()}
            };
            startWorkflowRequest.WorkflowDef = workflow;
            return startWorkflowRequest;
        }

        private static List<Conductor.Client.Models.TaskDef> GetTaskDefs(ConductorWorkflow workflow)
        {
            var taskDefs = new List<Conductor.Client.Models.TaskDef>();
            workflow.Tasks.ForEach(
                (task) =>
                {
                    taskDefs.Add(GetTaskDef(task));
                }
            );
            return taskDefs;
        }

        private static Conductor.Client.Models.TaskDef GetTaskDef(Conductor.Client.Models.WorkflowTask workflowTask)
        {
            return new Conductor.Client.Models.TaskDef(
                name: workflowTask.Name,
                ownerEmail: "example@orkes.io",
                timeoutSeconds: 0
            );
        }

        private static ConductorWorkflow CreateComplexWorkflow()
        {
            var getUserInfoTask = new SimpleTask("get_user_info", "get_user_info")
                    .WithInput("userId", "${workflow.input.userId}");
            var emailOrSmsTask = new SwitchTask("emailorsms", "${workflow.input.notificationPref}")
                    .WithDecisionCase(
                        WorkflowInput.NotificationPreference.EMAIL.ToString(),
                        new SimpleTask("send_email", "send_email")
                                .WithInput("email", "${get_user_info.output.email}")
                    )
                    .WithDecisionCase(
                        WorkflowInput.NotificationPreference.SMS.ToString(),
                        new SimpleTask("send_sms", "send_sms")
                                .WithInput("phoneNumber", "${get_user_info.output.phoneNumber}")
                    );
            return new ConductorWorkflow()
                .WithName("user_notification")
                .WithVersion(1)
                .WithInputParameter("userId")
                .WithInputParameter("notificationPref")
                .WithTask(getUserInfoTask, emailOrSmsTask);
        }
    }
}

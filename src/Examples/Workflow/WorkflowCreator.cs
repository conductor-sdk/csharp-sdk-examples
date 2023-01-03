using Conductor.Definition;
using Conductor.Definition.TaskType;

namespace Examples.Workflow
{
    public class WorkflowCreator
    {
        public static ConductorWorkflow CreateComplexWorkflow()
        {
            return new ConductorWorkflow()
                .WithName("user_notification")
                .WithVersion(1)
                .WithTask(
                    CreateGetUserDetailsTask(),
                    CreateEmailOrSMSTask()
                );
        }

        private static Task CreateGetUserDetailsTask()
        {
            return new SimpleTask("get_user_info", "get_user_info")
                    .WithInput("userId", "${workflow.input.userId}");
        }

        private static Task CreateEmailOrSMSTask()
        {
            return new SwitchTask("emailorsms", "${workflow.input.notificationPref}")
                    .WithDecisionCase("email", CreateSendEmailTask())
                    .WithDecisionCase("sms", CreateSendSMSTask());
        }

        private static Task CreateSendEmailTask()
        {
            return new SimpleTask("send_email", "send_email")
                    .WithInput("email", "${get_user_info.output.email}");
        }

        private static Task CreateSendSMSTask()
        {
            return new SimpleTask("send_sms", "send_sms")
                    .WithInput("phoneNumber", "${get_user_info.output.phoneNumber}");
        }
    }
}

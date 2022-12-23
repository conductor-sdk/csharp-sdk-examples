using Conductor.Definition;
using Conductor.Definition.TaskType;

ConductorWorkflow workflow = CreateWorkflow();
Examples.Workflow.WorkflowUtil.RegisterWorkflow(workflow);
Examples.Workflow.WorkflowUtil.StartWorkflowsAndValidateTheirCompletion();

static ConductorWorkflow CreateWorkflow()
{
    return new ConductorWorkflow()
        .WithName("email_send_workflow")
        .WithVersion(1)
        .WithTask(
            new SimpleTask("get_user_info", "get_user_info")
                .WithInput("userId", "${workflow.input.userId}"),
            new SimpleTask("send_email", "send_email")
                .WithInput("email", "${get_user_info.output.email}"));
}

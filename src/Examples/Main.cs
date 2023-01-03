using Conductor.Definition;

ConductorWorkflow workflow = Examples.Workflow.WorkflowCreator.CreateComplexWorkflow();
Examples.Workflow.WorkflowUtil.RegisterWorkflow(workflow);

Examples.Workflow.WorkflowUtil.StartWorkflowsAndValidateTheirCompletion();

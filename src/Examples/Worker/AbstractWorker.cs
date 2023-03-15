using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using Conductor.Client.Worker;

namespace Examples.Worker
{
    public abstract class AbstractWorker : IWorkflowTask
    {
        public string TaskType { get; }

        public WorkflowTaskExecutorConfiguration WorkerSettings { get; }

        public AbstractWorker(string taskType)
        {
            TaskType = taskType;
            WorkerSettings = new WorkflowTaskExecutorConfiguration();
        }

        public virtual TaskResult Execute(Task task)
        {
            return task.Completed();
        }
    }
}

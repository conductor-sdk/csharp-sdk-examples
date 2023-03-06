using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Worker
{
    public class UserTransactions : IWorkflowTask
    {
        public string TaskType { get; }
        public WorkflowTaskExecutorConfiguration WorkerSettings { get; } = new WorkflowTaskExecutorConfiguration();

        public UserTransactions(string taskType = "get_user_transactions")
        {
            TaskType = taskType;
        }

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            return await System.Threading.Tasks.Task.FromResult(task.Completed());
        }
    }
}

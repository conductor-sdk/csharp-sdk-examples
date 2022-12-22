using Conductor.Client.Models;
using Conductor.Client.Interfaces;
using Conductor.Client.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Worker
{
    public class SimpleWorker : IWorkflowTask
    {
        public string TaskType { get; }
        public int? Priority { get; }

        public SimpleWorker(string taskType = "csharp-sdk-example-task")
        {
            TaskType = taskType;
        }

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            return await System.Threading.Tasks.Task.FromResult(task.Completed());
        }
    }
}

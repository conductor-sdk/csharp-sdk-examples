using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Worker
{
    public class GetUserInfo : IWorkflowTask
    {
        public string TaskType { get; } = "get_user_info";
        public WorkflowTaskExecutorConfiguration WorkerSettings { get; } = new WorkflowTaskExecutorConfiguration();

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            TaskResult result = task.Completed(
                outputData: CreateOutputDataFromTask(task)
            );
            return await System.Threading.Tasks.Task.FromResult(result);
        }

        private Dictionary<string, object> CreateOutputDataFromTask(Conductor.Client.Models.Task task)
        {
            string userId = (string)task.InputData["userId"];
            UserInfo userInfo = new UserInfo("User X", userId);
            userInfo.Email = $"{userId}@example.com";
            userInfo.PhoneNumber = "555-555-5555";
            return new Dictionary<string, object>()
            {
                {"name", userInfo.Name},
                {"id", userInfo.Id},
                {"email", userInfo.Email},
                {"phoneNumber", userInfo.PhoneNumber}
            };
        }
    }
}

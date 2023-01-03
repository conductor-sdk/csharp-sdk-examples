using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Examples.Worker
{
    public class GetUserInfo : IWorkflowTask
    {
        public string TaskType { get; } = "get_user_info";
        public int? Priority { get; }

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            string userId = (string)task.InputData["userId"];
            UserInfo userInfo = new UserInfo("User X", userId);
            userInfo.Email = $"{userId}@example.com";
            userInfo.PhoneNumber = "555-555-5555";
            TaskResult result = task.Completed(
                outputData: GetOutputDataFromUserInfo(userInfo)
            );
            return await System.Threading.Tasks.Task.FromResult(result);
        }

        private Dictionary<string, object> GetOutputDataFromUserInfo(UserInfo userInfo)
        {
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

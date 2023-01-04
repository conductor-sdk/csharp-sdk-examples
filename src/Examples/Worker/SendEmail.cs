using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Worker
{
    public class SendEmail : IWorkflowTask
    {
        public string TaskType { get; } = "send_email";
        public int? Priority { get; }

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            string email = (string)task.InputData["email"];
            string message = $"Sent email to {email}";
            Console.WriteLine(message);
            TaskResult result = task.Completed();
            result.OutputData = new Dictionary<string, object>()
            {
                {"output_key", message}
            };
            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}

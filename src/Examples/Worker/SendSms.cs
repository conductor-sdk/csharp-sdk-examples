using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Worker
{
    public class SendSms : IWorkflowTask
    {
        public string TaskType { get; } = "send_sms";
        public WorkflowTaskExecutorConfiguration WorkerSettings { get; } = new WorkflowTaskExecutorConfiguration();

        public async Task<TaskResult> Execute(Conductor.Client.Models.Task task, CancellationToken token)
        {
            string phoneNumber = (string)task.InputData["phoneNumber"];
            string message = $"Sent sms to {phoneNumber}";
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

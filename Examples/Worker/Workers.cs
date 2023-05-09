using Conductor.Client.Extensions;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using System;
using System.Collections.Generic;

namespace Examples.Worker
{
    [WorkerTask]
    public class NotificationWorker
    {

        [WorkerTask("send_sms", 10, null, 100, "workerId")]
        public TaskResult SendSmsWorker(Task task)
        {
            var phoneNumber = (string)task.InputData["phoneNumber"];
            var message = $"Sent sms to {phoneNumber}";
            Console.WriteLine(message);
            var result = task.Completed();
            result.OutputData = new Dictionary<string, object>() { { "output_key", message } };
            return result;
        }

        [WorkerTask("send_email", 10, null, 100, "workerId")]
        public TaskResult SendEmailWorker(Task task)
        {
            var email = (string)task.InputData["email"];
            var message = $"Sent email to {email}";
            Console.WriteLine(message);
            var result = task.Completed();
            result.OutputData = new Dictionary<string, object>() { { "output_key", message } };
            return result;
        }
    }

    public class GetUserInfo : AbstractWorker
    {
        public GetUserInfo() : base("get_user_info") { }

        override
        public TaskResult Execute(Task task)
        {
            return task.Completed(CreateOutputDataFromTask(task));
        }

        private Dictionary<string, object> CreateOutputDataFromTask(Task task)
        {
            var userId = (string)task.InputData["userId"];
            var userInfo = new UserInfo("User X", userId);
            userInfo.Email = $"{userId}@example.com";
            userInfo.PhoneNumber = "555-555-5555";
            return GetDictionaryFromObject(userInfo);
        }
    }
}

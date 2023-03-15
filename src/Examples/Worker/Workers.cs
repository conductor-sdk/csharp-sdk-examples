using Conductor.Client.Extensions;
using Conductor.Client.Models;
using System;
using System.Collections.Generic;

namespace Examples.Worker
{
    public class UserTransactions : AbstractWorker
    {
        public UserTransactions(string taskType = "get_user_transactions") : base(taskType) { }
    }

    public class SendSms : AbstractWorker
    {
        public SendSms() : base("send_sms") { }

        override
        public TaskResult Execute(Task task)
        {
            string phoneNumber = (string)task.InputData["phoneNumber"];
            string message = $"Sent sms to {phoneNumber}";
            Console.WriteLine(message);
            TaskResult result = task.Completed();
            result.OutputData = new Dictionary<string, object>() { { "output_key", message } };
            return result;
        }
    }

    public class SendEmail : AbstractWorker
    {
        public SendEmail() : base("send_sms") { }

        override
        public TaskResult Execute(Task task)
        {
            string email = (string)task.InputData["email"];
            string message = $"Sent email to {email}";
            Console.WriteLine(message);
            TaskResult result = task.Completed();
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

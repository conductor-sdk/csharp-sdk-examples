using Conductor.Client.Extensions;
using Conductor.Client.Models;
using System;
using System.Collections.Generic;

namespace Examples.Worker
{
    public class SendSms : AbstractWorker
    {
        public SendSms() : base("send_sms") { }

        override
        public TaskResult Execute(Task task)
        {
            var phoneNumber = (string)task.InputData["phoneNumber"];
            var message = $"Sent sms to {phoneNumber}";
            Console.WriteLine(message);
            var result = task.Completed();
            result.OutputData = new Dictionary<string, object>() { { "output_key", message } };
            return result;
        }
    }

    public class SendEmail : AbstractWorker
    {
        public SendEmail() : base("send_email") { }

        override
        public TaskResult Execute(Task task)
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

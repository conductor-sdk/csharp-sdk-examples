using Conductor.Client.Extensions;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using Examples.Models;
using Examples.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.Worker
{
    [WorkerTask]
    public class Workers
    {
        private static Random _random;
        private static FraudCheckService _fraudCheckService;

        static Workers()
        {
            _random = new Random();
            _fraudCheckService = new FraudCheckService();
        }

        // docs-marker-start-1
        // Note: Using this setting, up to 5 tasks will run in parallel, with tasks being polled every 200ms
        [WorkerTask(taskType: "fraud-check", batchSize: 5, domain: null, pollIntervalMs: 200, workerId: "workerId")]
        public TaskResult FraudWorker(Task task)
        {
            var depositDetail = (DepositDetail)task.InputData["depositDetail"];
            var fraudCheckResult = _fraudCheckService.CheckForFraud(depositDetail);
            var result = task.Completed();
            result.OutputData = Examples.Util.TypeUtil.GetDictionaryFromObject(fraudCheckResult);
            return result;
        }

        // docs-marker-end-1

        // docs-marker-start-2
        [WorkerTask(taskType: "retrieve-deposit-batch", batchSize: 5, domain: null, pollIntervalMs: 200, workerId: "workerId")]
        public TaskResult RetrieveDepositBatch(Task task)
        {
            var batchCount = _random.Next(5, 11);
            if (task.InputData.ContainsKey("batchCount"))
            {
                batchCount = (int)task.InputData["batchCount"];
            }
            batchCount = Math.Min(100, batchCount); // Limit to 100 in playground
            var depositDetails = Enumerable.Range(0, batchCount)
                .Select(i => new DepositDetail
                {
                    accountId = $"acc-id-{i}",
                    amount = i * 1500L
                }).ToList();
            var result = task.Completed();
            Console.WriteLine($"Returning {depositDetails.Count} transactions");
            result.OutputData = new Dictionary<string, object> { { "depositDetails", depositDetails } };
            return result;
        }

        // docs-marker-end-2        
    }
}

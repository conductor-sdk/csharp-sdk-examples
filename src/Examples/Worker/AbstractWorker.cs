using Conductor.Client.Extensions;
using Conductor.Client.Interfaces;
using Conductor.Client.Models;
using Conductor.Client.Worker;
using System.Collections.Generic;

namespace Examples.Worker
{
    public abstract class AbstractWorker : IWorkflowTask
    {
        public string TaskType { get; }

        public WorkflowTaskExecutorConfiguration WorkerSettings { get; }

        public AbstractWorker(string taskType)
        {
            TaskType = taskType;
            WorkerSettings = new WorkflowTaskExecutorConfiguration();
        }

        public virtual TaskResult Execute(Task task)
        {
            return task.Completed();
        }

        protected virtual Dictionary<string, object> GetDictionaryFromObject<T>(T obj)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                dict[prop.Name] = prop.GetValue(obj);
            }
            return dict;
        }
    }
}

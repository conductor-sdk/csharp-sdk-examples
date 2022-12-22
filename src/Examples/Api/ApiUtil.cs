using Conductor.Api;
using Conductor.Client;
using Conductor.Executor;
using System;
using System.Diagnostics;

namespace Examples.Api
{
    public class ApiUtil
    {
        private const string KEY = "KEY";
        private const string SECRET = "SECRET";
        private const string CONDUCTOR_SERVER_URL = "CONDUCTOR_SERVER_URL";

        private static string _basePath = null;
        private static string _keyId = null;
        private static string _keySecret = null;

        static ApiUtil()
        {
            _keyId = GetEnvironmentVariable(KEY);
            _keySecret = GetEnvironmentVariable(SECRET);
            _basePath = GetEnvironmentVariable(CONDUCTOR_SERVER_URL);
        }

        public static WorkflowExecutor GetWorkflowExecutor()
        {
            return new WorkflowExecutor(
                metadataClient: GetClient<MetadataResourceApi>(),
                workflowClient: GetClient<WorkflowResourceApi>()
            );
        }

        public static T GetClient<T>() where T : IApiAccessor, new()
        {
            OrkesApiClient apiClient = new OrkesApiClient(GetConfiguration());
            return apiClient.GetClient<T>();
        }

        public static Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration();
            configuration.keyId = _keyId;
            configuration.keySecret = _keySecret;
            configuration.BasePath = _basePath;
            return configuration;
        }

        private static string GetEnvironmentVariable(string variable)
        {
            string value = Environment.GetEnvironmentVariable(variable);
            Debug.Assert(value != null);
            return value;
        }
    }
}

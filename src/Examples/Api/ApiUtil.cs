using Conductor.Api;
using Conductor.Client;
using Conductor.Client.Authentication;
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

        private static Configuration _configuration = null;

        static ApiUtil()
        {
            _keyId = GetEnvironmentVariable(KEY);
            _keySecret = GetEnvironmentVariable(SECRET);
            _basePath = GetEnvironmentVariable(CONDUCTOR_SERVER_URL);
            _configuration = GetConfiguration();
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
            return _configuration.GetClient<T>();
        }

        public static Configuration GetConfiguration()
        {
            var configuration = new Configuration();
            configuration.Timeout = 20000;
            configuration.BasePath = _basePath;
            configuration.AuthenticationSettings = new OrkesAuthenticationSettings(_keyId, _keySecret);
            return configuration;
        }

        public static string GetWorkflowExecutionURL(string workflowId)
        {
            var prefix = _basePath.Remove(_basePath.Length - 4);
            return $"{prefix}/execution/{workflowId}";
        }

        private static string GetEnvironmentVariable(string variable)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            Debug.Assert(value != null);
            return value;
        }
    }
}

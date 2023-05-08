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
        private const string CONDUCTOR_SERVER_URL = "CONDUCTOR_SERVER_URL";

        private static string _basePath = null;

        static ApiUtil()
        {
            _basePath = GetEnvironmentVariable(CONDUCTOR_SERVER_URL);
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

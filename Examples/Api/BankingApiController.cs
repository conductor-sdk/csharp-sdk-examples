using Examples.Models;
using Examples.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Conductor.Client.Extensions;

namespace Examples.Api
{
    public class BankingApiController : ControllerBase
    {
        private static readonly FraudCheckService _fraudCheckService;
        private static readonly WorkflowService _workflowService;

        static BankingApiController()
        {
            _fraudCheckService = new FraudCheckService();
            _workflowService = new WorkflowService();
        }

        [HttpPost("checkForFraud")]
        public ActionResult<FraudCheckResult> CheckForFraud([FromBody] DepositDetail depositDetail)
        {
            var fraudCheckResult = _fraudCheckService.CheckForFraud(depositDetail);
            Console.WriteLine($"Checked for fraud, deposit: {depositDetail}, result: {fraudCheckResult}");
            return Ok(fraudCheckResult);
        }

        // docs-marker-start-1
        [HttpPost("triggerDepositFlow")]
        public ActionResult<Dictionary<string, object>> TriggerDepositWorkflow([FromBody] DepositDetail depositDetail)
        {

            var workflowId = _workflowService.StartDepositWorkflow(depositDetail);
            Console.WriteLine($"Triggered deposit workflow, depositDetail: {depositDetail}, workflowExecution: {ApiExtensions.GetWorkflowExecutionURL(workflowId)}");
            var response = new Dictionary<string, object> { { "workflowId", workflowId } };
            return Ok(response);
        }
        // docs-marker-end-1
    }
}

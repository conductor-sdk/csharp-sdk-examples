using Examples.Models;

namespace Examples.Service
{
    public class FraudCheckService
    {
        public FraudCheckResult CheckForFraud(DepositDetail depositDetail)
        {
            var fraudCheckResult = new FraudCheckResult();
            if (depositDetail.amount > 100 * 1000)
            {
                fraudCheckResult.result = FraudCheckResult.Result.FAIL;
                fraudCheckResult.reason = "Amount above threshold";
            }
            else
            {
                fraudCheckResult.result = FraudCheckResult.Result.PASS;
                fraudCheckResult.reason = "All good!";
            }
            return fraudCheckResult;
        }
    }
}

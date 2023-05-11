namespace Examples.Models
{
    public class FraudCheckResult
    {
        public enum Result
        {
            PASS, FAIL
        }

        public Result result { get; set; }

        public string reason { get; set; }
    }
}

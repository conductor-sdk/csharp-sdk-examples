namespace Examples.Workflow
{
    public class WorkflowInput
    {
        public enum NotificationPreference
        {
            EMAIL, SMS
        }
        public string UserId { get; set; }

        public NotificationPreference NotificationPref { get; set; } = NotificationPreference.EMAIL;

        public WorkflowInput(string userId)
        {
            UserId = userId;
        }
    }
}
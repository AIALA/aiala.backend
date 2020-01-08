using aiala.Backend.Data.Schedule;

namespace aiala.Backend.Data.Activities
{
    public class ScheduledTaskActivity : Activity
    {
        public const string FeedbackDataKey = "TaskFeedback";

        public const string TaskDelayedUntilKey = "TaskDelayedUntil";

        /// <summary>
        /// The task that is the context of the activity
        /// </summary>
        public ScheduledTask Task { get; set; }
    }
}

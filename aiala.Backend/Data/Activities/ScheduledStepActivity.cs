using aiala.Backend.Data.Schedule;

namespace aiala.Backend.Data.Activities
{
    public class ScheduledStepActivity : Activity
    {
        public const string StateDataKey = "StepType";

        public ScheduledStep Step { get; set; }
    }
}

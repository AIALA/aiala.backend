using System;

namespace aiala.Backend.Data.Schedule
{
    public class ScheduledStep
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public StepState State { get; set; }

        public ScheduledTask Task { get; set; }
    }
}
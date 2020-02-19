using System;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.ApiModels.Schedule
{
    public class ScheduledStepModel
    {
        public Guid? Id { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public StepState State { get; set; }
    }
}
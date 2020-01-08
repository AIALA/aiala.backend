using System;

namespace aiala.Backend.Models.Schedule
{
    public class UpsertedDayTask : IScheduledTaskValidationModel
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}

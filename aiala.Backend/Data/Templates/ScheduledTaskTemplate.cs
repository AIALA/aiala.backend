using System;
using aiala.Backend.Data.Tasks;
using aiala.Backend.Models.Schedule;

namespace aiala.Backend.Data.Templates
{
    public class ScheduledTaskTemplate : IScheduledTaskValidationModel
    {
        public Guid Id { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public Guid TaskId { get; set; }

        public AppTask Task { get; set; }

        public DayTemplate DayTemplate { get; set; }
    }
}
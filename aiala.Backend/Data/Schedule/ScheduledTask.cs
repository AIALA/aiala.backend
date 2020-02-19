using System;
using System.Collections.Generic;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Tasks;

namespace aiala.Backend.Data.Schedule
{
    public class ScheduledTask
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TimeSpan DefaultDuration { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public TimeSpan ExpirationOffset { get; set; }

        public TaskFeedback Feedback { get; set; }

        public bool UseTaskContacts { get; set; }

        public string FreeFormPlace { get; set; }

        public Picture Picture { get; set; }

        public Account EmergencyContact1 { get; set; }

        public Account EmergencyContact2 { get; set; }

        public AppTask Task { get; set; }

        public ScheduledPlace Place { get; set; }

        public ICollection<ScheduledStep> Steps { get; set; }

        public Day Day { get; set; }
    }
}
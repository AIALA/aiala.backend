using System;
using System.Collections.Generic;

namespace aiala.Backend.Data.Schedule
{
    public class Day
    {
        public Guid Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Name { get; set; }

        public ICollection<ScheduledTask> Tasks { get; set; }

        public Tenant Tenant { get; set; }
    }
}

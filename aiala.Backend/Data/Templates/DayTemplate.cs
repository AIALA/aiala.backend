using System;
using System.Collections.Generic;

namespace aiala.Backend.Data.Templates
{
    public class DayTemplate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DayName { get; set; }

        public ICollection<ScheduledTaskTemplate> Tasks { get; set; }

        public Tenant Tenant { get; set; }
    }
}

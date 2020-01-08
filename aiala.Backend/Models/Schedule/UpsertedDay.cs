using System;
using System.Collections.Generic;

namespace aiala.Backend.Models.Schedule
{
    public class UpsertedDay
    {
        public Guid Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Name { get; set; }

        public ICollection<UpsertedDayTask> Tasks { get; set; }
    }
}

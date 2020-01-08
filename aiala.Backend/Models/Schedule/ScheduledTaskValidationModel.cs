using System;
using System.Collections.Generic;

namespace aiala.Backend.Models.Schedule
{
    public class ScheduledTasksValidationModel
    {
        public ScheduledTasksValidationModel(
            object returnValue,
            IEnumerable<IScheduledTaskValidationModel> tasks,
            DateTimeOffset? dayDate)
        {
            ReturnValue = returnValue;
            Tasks = tasks;
            DayDate = dayDate;
        }

        public object ReturnValue { get; }

        public IEnumerable<IScheduledTaskValidationModel> Tasks { get; }

        public DateTimeOffset? DayDate { get; }
    }
}

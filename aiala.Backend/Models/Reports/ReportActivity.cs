using aiala.Backend.Data.Activities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace aiala.Backend.Models.Reports
{
    public class ReportActivity
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }

        public string ActivityData { get; set; }

        public string Date { get; set; }

        public int TimeOfDayMinutes { get; set; }

        public string DayOfWeek { get; set; }

        public string DayName { get; set; }

        public string TaskName { get; set; }

        public int? TaskDuration { get; set; }

        public int? PercentageOfTaskLate { get; set; }

        public bool? WasCompletedLate { get; set; }

        public int? StepsCount { get; set; }

        public int? AverageStepTextLength { get; set; }
    }
}

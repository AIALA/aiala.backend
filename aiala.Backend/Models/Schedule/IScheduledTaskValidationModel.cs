using System;

namespace aiala.Backend.Models.Schedule
{
    public interface IScheduledTaskValidationModel
    {
        Guid Id { get; set; }

        TimeSpan Start { get; set; }

        TimeSpan End { get; set; }
    }
}

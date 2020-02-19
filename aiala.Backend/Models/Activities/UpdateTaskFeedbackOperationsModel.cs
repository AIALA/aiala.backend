using System;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.Models.Activities
{
    public class UpdateTaskFeedbackOperationsModel
    {
        public Guid TaskId { get; set; }

        public TaskFeedback NewTaskFeedback { get; set; }
    }
}

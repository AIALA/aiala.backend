using System;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.Models.Activities
{
    public class UpdateStepStateOperationsModel
    {
        public Guid StepId { get; set; }

        public StepState NewStepState { get; set; }
    }
}

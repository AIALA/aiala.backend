using aiala.Backend.Data.Schedule;

namespace aiala.Backend.ApiModels.Activities
{
    public class UpdateTaskFeedbackModel : ActivityMetadataModel
    {
        public TaskFeedback Feedback { get; set; }
    }
}

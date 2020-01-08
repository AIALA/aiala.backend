using aiala.Backend.Data.Schedule;

namespace aiala.Backend.ApiModels.Activities
{
    public class UpdateStepStateModel : ActivityMetadataModel
    {
        public StepState State { get; set; }
    }
}

using aiala.Backend.Data.Activities;

namespace aiala.Backend.ApiModels.Activities
{
    public class UpdateEmergencyMoodModel : ActivityMetadataModel
    {
        public EmergencyMood Mood { get; set; }
    }
}

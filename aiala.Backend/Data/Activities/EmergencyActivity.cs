using System;

namespace aiala.Backend.Data.Activities
{
    public class EmergencyActivity : Activity
    {
        public const string MoodDataKey = "EmergencyMood";

        /// <summary>
        /// The emergency ID.
        /// There is no emergency entity, the emergency is defined by the activities which share an emergency ID.
        /// </summary>
        public Guid EmergencyId { get; set; }
    }
}

using System.Collections.Generic;
using aiala.Backend.ApiModels.Places;

namespace aiala.Backend.ApiModels.Directory
{
    public class MobileAppSettingsModel
    {
        public List<EmergencyContactModel> EmergencyContacts { get; set; }

        public List<PlaceAppModel> Places { get; set; }

        public string EmergencyTextBad { get; set; }

        public string EmergencyTextImproving { get; set; }
    }
}

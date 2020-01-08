using System.Collections.Generic;

namespace aiala.Backend.Options
{
    public class AzureVisionOptions
    {
        public string Key { get; set; }

        public float TagsConfidenceThreshold { get; set; }

        public float CaptionsConfidenceThreshold { get; set; }

        public List<string> TagBlacklist { get; set; }
    }
}

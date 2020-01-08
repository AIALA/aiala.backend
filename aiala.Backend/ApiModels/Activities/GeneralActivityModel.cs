using System.Collections.Generic;

namespace aiala.Backend.ApiModels.Activities
{
    public class GeneralActivityModel : ActivityMetadataModel
    {
        public Dictionary<string, string> ActivityData { get; set; }
    }
}

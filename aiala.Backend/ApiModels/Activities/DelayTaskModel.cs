using System;

namespace aiala.Backend.ApiModels.Activities
{
    public class DelayTaskModel : ActivityMetadataModel
    {
        public TimeSpan DelayedUntil { get; set; }
    }
}

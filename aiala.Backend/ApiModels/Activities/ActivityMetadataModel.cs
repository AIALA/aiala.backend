using System;

namespace aiala.Backend.ApiModels.Activities
{
    public class ActivityMetadataModel
    {
        public DateTimeOffset Timestamp { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public Guid? ActiveTaskId { get; set; }

        public Guid? ActiveStepId { get; set; }
    }
}

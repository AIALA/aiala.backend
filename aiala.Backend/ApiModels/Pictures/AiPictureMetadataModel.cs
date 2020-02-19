using System;
using System.Collections.Generic;

namespace aiala.Backend.ApiModels.Pictures
{
    public class AiPictureMetadataModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public float DescriptionConfidence { get; set; }

        public List<AiPictureTagModel> Tags { get; set; }
    }
}

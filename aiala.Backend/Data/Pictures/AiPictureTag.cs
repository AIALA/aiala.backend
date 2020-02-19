using System;

namespace aiala.Backend.Data.Pictures
{
    public class AiPictureTag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool HasHumanConfidence { get; set; }

        public float Confidence { get; set; }

        public AiPictureMetadata Picture { get; set; }
    }
}

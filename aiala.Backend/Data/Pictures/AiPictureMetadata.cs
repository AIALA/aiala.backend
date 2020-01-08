using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aiala.Backend.Data.Pictures
{
    public class AiPictureMetadata
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool HasHumanConfidence { get; set; }

        public float DescriptionConfidence { get; set; }

        public ICollection<AiPictureTag> Tags { get; set; }

        public Guid PictureId { get; set; }

        public Picture Picture { get; set; }
    }
}

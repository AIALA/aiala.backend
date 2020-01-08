using System.Collections.Generic;

namespace aiala.Backend.ApiModels.Refit
{
    public class VisionAnalysisModel
    {
        public List<VisionTag> Tags { get; set; }

        public VisionDescription Description { get; set; }
    }

    public class VisionTag
    {
        public string Name { get; set; }

        public float Confidence { get; set; }
    }

    public class VisionDescription
    {
        public List<string> Tags { get; set; }

        public List<VisionCaption> Captions { get; set; }
    }

    public class VisionCaption
    {
        public string Text { get; set; }

        public float Confidence { get; set; }
    }
}

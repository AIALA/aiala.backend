using System;

namespace aiala.Backend.ApiModels.Pictures
{
    public class AiPictureTagModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Confidence { get; set; }
    }
}

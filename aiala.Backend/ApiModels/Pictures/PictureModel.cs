using System;

namespace aiala.Backend.ApiModels.Pictures
{
    public class PictureModel
    {
        public Guid? Id { get; set; }

        public string PictureUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public AiPictureMetadataModel AiMetadata { get; set; }
    }
}

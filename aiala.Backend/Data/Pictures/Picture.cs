using System;
using System.Collections.Generic;

namespace aiala.Backend.Data.Pictures
{
    public class Picture
    {
        public Guid Id { get; set; }

        public PictureType PictureType { get; set; }

        public string StorageDirectLink { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Tenant Tenant { get; set; }

        public AiPictureMetadata AiMetadata { get; set; }
    }
}

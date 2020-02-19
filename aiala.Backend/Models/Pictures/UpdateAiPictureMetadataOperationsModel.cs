using System;
using System.Collections.Generic;

namespace aiala.Backend.Models.Pictures
{
    public class UpdateAiPictureMetadataOperationsModel
    {
        public Guid PictureId { get; set; }

        public string Description { get; set; }

        public List<string> AddedTags { get; set; }

        public List<UpdateAiPictureTagOperationsModel> UpdatedTags { get; set; }

        public List<Guid> RemovedTags { get; set; }
    }
}

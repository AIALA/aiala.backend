using System;
using System.Collections.Generic;

namespace aiala.Backend.ApiModels.Pictures
{
    public class UpdatePictureModel
    {
        public string Description { get; set; }

        public List<string> AddedTags { get; set; }

        public List<UpdatePictureTagModel> UpdatedTags { get; set; }

        public List<Guid> RemovedTags { get; set; }
    }
}

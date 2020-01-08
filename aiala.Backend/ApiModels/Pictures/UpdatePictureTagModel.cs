using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aiala.Backend.ApiModels.Pictures
{
    public class UpdatePictureTagModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

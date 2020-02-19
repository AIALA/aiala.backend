using aiala.Backend.Models.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aiala.Backend.ApiModels.Directory
{
    public class LocalizationSettings
    {
        public TimeSpan TimeZoneOffset { get; set; }

        public AppCulture TenantCulture { get; set; }
    }
}

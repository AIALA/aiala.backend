using aiala.Backend.Models.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aiala.Backend.ApiModels.Documents
{
    public class DayOtpModel
    {
        public DateTimeOffset Date { get; set; }

        public AppCulture Culture { get; set; }
    }
}

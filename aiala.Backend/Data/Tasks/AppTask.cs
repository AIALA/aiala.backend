using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Places;

namespace aiala.Backend.Data.Tasks
{
    [Table("Tasks")]
    public class AppTask
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public bool UseTaskContacts { get; set; }

        public string FreeFormPlace { get; set; }

        public bool IsDeleted { get; set; }

        public Picture Picture { get; set; }

        public ICollection<Step> Steps { get; set; }

        public Place Place { get; set; }

        public Account EmergencyContact1 { get; set; }

        public Account EmergencyContact2 { get; set; }

        /// <summary>
        /// The account who last edited this task.
        /// </summary>
        public Account Author { get; set; }

        public Tenant Tenant { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.Data.Activities
{
    public class Activity
    {
        public Guid Id { get; set; }

        public ActivityType Type { get; set; }

        /// <summary>
        /// JSON seialized dictionary for miscellaneous data
        /// </summary>
        public string ActivityData { get; set; }

        /// <summary>
        /// Time created on server
        /// </summary>
        public DateTimeOffset TimeCreated { get; set; }

        /// <summary>
        /// Timestamp of activity as it happened on client
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Longitude { get; set; }

        public Tenant Tenant { get; set; }

        /// <summary>
        /// The task which was currently active when the activity happened.
        /// Null of none active.
        /// </summary>
        public ScheduledTask ActiveTask { get; set; }

        /// <summary>
        /// The step which was currently active when the activity happened.
        /// Null of none active.
        /// </summary>
        public ScheduledStep ActiveStep { get; set; }
    }
}

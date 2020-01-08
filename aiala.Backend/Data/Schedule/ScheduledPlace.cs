using System;
using System.ComponentModel.DataAnnotations.Schema;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Places;

namespace aiala.Backend.Data.Schedule
{
    public class ScheduledPlace
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        public Guid TaskId { get; set; }

        public Picture Picture { get; set; }

        public ScheduledTask Task { get; set; }

        public Place Place { get; set; }
    }
}

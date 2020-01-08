using aiala.Backend.Data.Pictures;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aiala.Backend.Data.Places
{
    public class Place
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        public bool IsDeleted { get; set; }

        public Picture Picture { get; set; }

        public Tenant Tenant { get; set; }
    }
}

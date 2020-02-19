using aiala.Backend.ApiModels.Pictures;
using System;

namespace aiala.Backend.ApiModels.Places
{
    public class PlaceModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public PictureModel Picture { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}

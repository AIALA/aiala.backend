using aiala.Backend.ApiModels.Pictures;

namespace aiala.Backend.ApiModels.Places
{
    public class PlaceAppModel
    {
        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public PictureModel Picture { get; set; }
    }
}

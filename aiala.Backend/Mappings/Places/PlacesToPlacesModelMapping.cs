using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Places;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Places;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Services;
using xappido.Operations;

namespace aiala.Backend.Mappings.Places
{
    public class PlaceToPlaceModelMapping : ModelMapping<Place, PlaceModel>
    {
        private readonly IPictureHelperService _pictureHelperService;
        private readonly AppDbContext _dbContext;
        private readonly PictureToPictureModelMapping _pictureMapping;

        public PlaceToPlaceModelMapping(IPictureHelperService pictureHelperService, AppDbContext dbContext, PictureToPictureModelMapping pictureMapping)
        {
            _pictureHelperService = pictureHelperService;
            _dbContext = dbContext;
            _pictureMapping = pictureMapping;
        }

        protected override async Task<PlaceModel> OnMap(Place input, OperationContext context = null)
        {
            return new PlaceModel
            {
                Id = input.Id,
                Name = input.Name,
                Picture = input.Picture != null ? await _pictureMapping.Map(input.Picture) : _pictureHelperService.GetDefaultPictureModel(PictureType.PlacePictures),
                Latitude = input.Latitude,
                Longitude = input.Longitude
            };
        }

        protected override async Task<Place> OnMap(PlaceModel input, OperationContext context = null)
        {
            return new Place
            {
                Id = input.Id ?? Guid.NewGuid(),
                Name = input.Name,
                Picture = input.Picture?.Id != null ? await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == input.Picture.Id) : null,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                IsDeleted = false
            };
        }
    }
}

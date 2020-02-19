using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Tasks;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Tasks;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Services;
using xappido.Operations;

namespace aiala.Backend.Mappings.Tasks
{
    public class AppTaskToAppTaskModelMapping : ModelMapping<AppTask, AppTaskModel>
    {
        private readonly IPictureHelperService _pictureHelper;
        private readonly AppDbContext _dbContext;
        private readonly PictureToPictureModelMapping _pictureMapping;

        public AppTaskToAppTaskModelMapping(IPictureHelperService pictureHelper, AppDbContext dbContext, PictureToPictureModelMapping pictureMapping)
        {
            _pictureHelper = pictureHelper;
            _dbContext = dbContext;
            _pictureMapping = pictureMapping;
        }

        protected override async Task<AppTaskModel> OnMap(AppTask input, OperationContext context = null)
        {
            return new AppTaskModel
            {
                Id = input.Id,
                Duration = input.Duration,
                PlaceId = input.Place?.Id,
                Name = input.Name,
                UseTaskContacts = input.UseTaskContacts,
                FreeFormPlace = input.FreeFormPlace,
                Picture = input.Picture != null ? await _pictureMapping.Map(input.Picture) : _pictureHelper.GetDefaultPictureModel(PictureType.TaskPictures),
                EmergencyContact1Id = input.EmergencyContact1?.Id,
                EmergencyContact2Id = input.EmergencyContact2?.Id,
                Steps = input.Steps.Select(s => new StepModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    Order = s.Order
                })
            };
        }

        protected override async Task<AppTask> OnMap(AppTaskModel input, OperationContext context = null)
        {
            var task = new AppTask
            {
                Id = input.Id ?? Guid.Empty,
                Duration = input.Duration,
                Name = input.Name,
                Picture = input.Picture?.Id != null ? await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == input.Picture.Id) : null,
                UseTaskContacts = input.UseTaskContacts,
                FreeFormPlace = input.FreeFormPlace,
                Steps = input.Steps.Select(s => new Step
                {
                    Id = s.Id ?? Guid.Empty,
                    Text = s.Text,
                    Order = s.Order
                }).ToList()
            };

            task.Place = input.PlaceId.HasValue
                ? await _dbContext.Places.FirstOrDefaultAsync(p => p.Id == input.PlaceId)
                : null;
            task.EmergencyContact1 = input.EmergencyContact1Id.HasValue
                ? await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == input.EmergencyContact1Id)
                : null;
            task.EmergencyContact2 = input.EmergencyContact2Id.HasValue
                ? await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == input.EmergencyContact2Id)
                : null;

            return task;
        }
    }
}

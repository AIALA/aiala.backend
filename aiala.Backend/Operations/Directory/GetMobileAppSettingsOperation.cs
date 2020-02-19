using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.ApiModels.Places;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Models.Directory;
using aiala.Backend.Services;
using xappido.Directory.Services;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Operations.Directory
{
    public class GetMobileAppSettingsOperation : OutputOperation<MobileAppSettingsModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly ISettingsService _settingsService;
        private readonly IProfileService _profileService;
        private readonly IPictureHelperService _pictureHelperService;

        public GetMobileAppSettingsOperation(
            AppDbContext dbContext,
            ISettingsService settingsService,
            IProfileService profileService,
            IPictureHelperService pictureHelperService)
        {
            _dbContext = dbContext;
            _settingsService = settingsService;
            _profileService = profileService;
            _pictureHelperService = pictureHelperService;
        }

        protected override async Task<IOperationResult> Execute()
        {
            var tenantSettings = await _settingsService.Load<TenantSettings>(Context.TenantId.Value);

            var contacts = await _dbContext.Accounts
                .Where(a => a.Id == tenantSettings.EmergencyContact1Id || a.Id == tenantSettings.EmergencyContact2Id)
                .Select(a => new EmergencyContactModel
                {
                    Name = $"{a.User.Firstname} {a.User.Lastname}",
                    PictureUrl = _profileService.GetPictureUrl(a.Id)
                })
                .ToListAsync();

            var places = await _dbContext.Places
                .Where(p => p.Id == tenantSettings.Place1Id || p.Id == tenantSettings.Place2Id)
                .Select(p => new PlaceAppModel
                {
                    Name = p.Name,
                    Picture = p.Picture != null ? GetMapping<PictureToPictureModelMapping>().Map(p.Picture, Context).Result : _pictureHelperService.GetDefaultPictureModel(PictureType.PlacePictures),
                    Latitude = p.Latitude,
                    Longitude = p.Longitude
                })
                .ToListAsync();

            var emergencySettingsModel = new MobileAppSettingsModel
            {
                EmergencyContacts = contacts,
                Places = places,
                EmergencyTextBad = tenantSettings.EmergencyTextBad,
                EmergencyTextImproving = tenantSettings.EmergencyTextImproving
            };

            return Succeeded(emergencySettingsModel);
        }
    }
}

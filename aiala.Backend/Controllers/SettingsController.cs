using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Models.Directory;
using aiala.Backend.Operations.Directory;
using xappido.Authorization;
using xappido.Authorization.Attributes;
using xappido.Authorization.Policies;
using xappido.Directory.Settings.Controllers;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    public class SettingsController : SettingsController<AccountSettings, TenantSettings, UserSettings, UserSettingsModel>
    {
        private readonly ISettingsService _settingsService;
        private readonly IOperationExecutor _executor;

        public SettingsController(ISettingsService settingsService, IOperationExecutor executor) : base(settingsService)
        {
            _settingsService = settingsService;
            _executor = executor;
        }

        [ProducesResponseType(typeof(UserSettingsModel), 200)]
        public override Task<IActionResult> GetUserSettings() => base.GetUserSettings();

        [ProducesResponseType(typeof(UserSettingsModel), 200)]
        public override Task<IActionResult> SaveUserSettings([FromBody] UserSettingsModel settings) => base.SaveUserSettings(settings);

        [ProducesResponseType(typeof(TenantSettings), 200)]
        public override Task<IActionResult> GetTenantSettings() => base.GetTenantSettings();

        [ProducesResponseType(typeof(TenantSettings), 200)]
        public override Task<IActionResult> SaveTenantSettings([FromBody] TenantSettings settings) => base.SaveTenantSettings(settings);

        [AuthorizePolicy(typeof(ScheduleConsumptionPolicy))]
        [HttpGet("app")]
        [ProducesResponseType(typeof(MobileAppSettingsModel), 200)]
        public async Task<IActionResult> GetMobileAppSettings()
        {
            var result = await _executor
                .Add<GetMobileAppSettingsOperation>()
                .Execute();

            return result.CreateHttpResult();
        }

        [AuthorizePolicy(typeof(ValidUserPolicy))]
        [HttpGet("l10n")]
        [ProducesResponseType(typeof(LocalizationSettings), 200)]
        public async Task<IActionResult> GetL10nSettings()
        {
            var settings = await _settingsService.Load<TenantSettings>(User.GetTenantId().Value);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);

            return new OkObjectResult(new LocalizationSettings
            {
                TimeZoneOffset = timeZoneInfo.GetUtcOffset(DateTime.UtcNow),
                TenantCulture = settings.TenantCulture
            });
        }
    }
}

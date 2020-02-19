using aiala.Backend.ApiModels.Directory;
using aiala.Backend.Data;
using aiala.Backend.Models.Directory;
using System.Globalization;
using xappido.Directory.Controllers;
using xappido.Directory.Domain;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    public class RegistrationController : RegistrationController<AppDbContext, Tenant, Account, User, AppRegisterModel>
    {
        private readonly ISettingsService _settingsService;

        public RegistrationController(IOperationExecutor executor, ISettingsService settingsService) : base(executor)
        {
            _settingsService = settingsService;
        }

        protected override void CompleteRegistration(AppRegisterModel registerModel, Account createdAccount)
        {
            createdAccount.Tenant.TenantType = TenantType.MultiUser;
            createdAccount.Tenant.Culture = registerModel.Culture;

            _settingsService.Save(
                new TenantSettings
                {
                    TimeZoneId = registerModel.TimeZoneId,
                    TenantCulture = new CultureInfo(registerModel.Culture).ToAppCulture()
                },
                createdAccount.Tenant.Id);

            base.CompleteRegistration(registerModel, createdAccount);
        }
    }
}

using aiala.Backend.Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization;
using xappido.Directory.PermissionGroups;
using xappido.Directory.Settings.Authorization;

namespace aiala.Backend.Authorization.PermissionGroups
{
    public class WebAppUserPermissionGroup : IPermissionGroupConfiguration
    {
        public WebAppUserPermissionGroup()
        {
            Permissions = new IApplicationPermission[]
            {
                new GetSchedulePermission(),
                new ScheduleManagementPermission(),
                new ManageTenantSettingsPermission()
            };
        }

        public Guid Id { get; } = Guid.Parse("BE77A3DC-3504-40F1-9DDB-A58BED4F22F7");

        public string Name { get; } = "Web App User";

        public Guid? TenantId { get; }

        public Guid? AppId { get; }

        public bool AssignOnRegistration { get; } = false;

        public IReadOnlyCollection<IApplicationPermission> Permissions { get; }
    }
}

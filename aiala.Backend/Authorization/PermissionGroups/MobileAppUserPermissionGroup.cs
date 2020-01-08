using aiala.Backend.Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization;
using xappido.Directory.PermissionGroups;

namespace aiala.Backend.Authorization.PermissionGroups
{
    public class MobileAppUserPermissionGroup : IPermissionGroupConfiguration
    {
        public MobileAppUserPermissionGroup()
        {
            Permissions = new IApplicationPermission[]
            {
                new GetSchedulePermission(),
                new ScheduleConsumptionPermission()
            };
        }

        public Guid Id { get; } = Guid.Parse("457A8EC6-969F-4AED-A41D-85CA68F37F52");

        public string Name { get; } = "Mobile App User";

        public Guid? TenantId { get; }

        public Guid? AppId { get; }

        public bool AssignOnRegistration { get; } = true;

        public IReadOnlyCollection<IApplicationPermission> Permissions { get; }
    }
}

using aiala.Backend.Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization;
using xappido.Directory.PermissionGroups;

namespace aiala.Backend.Authorization.PermissionGroups
{
    public class AppAdministratorPermissionGroup : IPermissionGroupConfiguration
    {
        public AppAdministratorPermissionGroup(IPermissionService permissionService)
        {
            Permissions = permissionService.Get()
                .Where(p => p.GetType() != typeof(ScheduleConsumptionPermission))
                .ToList()
                .AsReadOnly();
        }

        public Guid Id { get; } = Guid.Parse("c3217597-9837-4c6d-b573-dad82d988400");

        public string Name { get; } = "Web Administrator";

        public Guid? TenantId { get; } = null;

        public Guid? AppId { get; } = null;

        public bool AssignOnRegistration { get; } = true;

        public IReadOnlyCollection<IApplicationPermission> Permissions { get; }
    }
}

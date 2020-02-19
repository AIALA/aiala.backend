using System.Collections.Generic;
using System.Collections.ObjectModel;
using aiala.Backend.Authorization.Permissions;
using xappido.Authorization;

namespace aiala.Backend.Authorization.Features
{
    public class ScheduleManagementFeature : IApplicationFeature
    {
        public ScheduleManagementFeature()
        {
            Permissions = new ReadOnlyCollection<IApplicationPermission>(new List<IApplicationPermission>
            {
                new ScheduleManagementPermission(),
                new GetSchedulePermission()
            });
        }

        public string Description => "Manage the schedule for the tenant";

        public ReadOnlyCollection<IApplicationPermission> Permissions { get; }
    }
}

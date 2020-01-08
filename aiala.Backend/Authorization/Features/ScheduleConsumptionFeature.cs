using System.Collections.Generic;
using System.Collections.ObjectModel;
using aiala.Backend.Authorization.Permissions;
using xappido.Authorization;

namespace aiala.Backend.Authorization.Features
{
    public class ScheduleConsumptionFeature : IApplicationFeature
    {
        public ScheduleConsumptionFeature()
        {
            Permissions = new ReadOnlyCollection<IApplicationPermission>(new List<IApplicationPermission>
            {
                new GetSchedulePermission(),
                new ScheduleConsumptionPermission()
            });
        }

        public string Description => "Get the schedule and update the state of its entities.";

        public ReadOnlyCollection<IApplicationPermission> Permissions { get; }
    }
}

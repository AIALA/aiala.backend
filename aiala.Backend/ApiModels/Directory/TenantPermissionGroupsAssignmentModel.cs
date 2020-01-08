using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Directory.ApiModels.PermissionGroups;

namespace aiala.Backend.ApiModels.Directory
{
    public class TenantPermissionGroupsAssignmentModel : Dictionary<Guid, List<PermissionGroupsAssignmentModel>>
    {
    }
}

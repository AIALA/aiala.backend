using aiala.Backend.Data.Pictures;
using System.Collections.Generic;
using xappido.Directory.Domain;

namespace aiala.Backend.Data
{
    public class Account : DirectoryAccount<Tenant, User>
    {
        public string PhoneNumber { get; set; }

        public ICollection<PermissionGroupAssignment<Tenant, Account, User>> PermissionGroupAssignments { get; set; }

        public Picture Picture { get; set; }
    }
}
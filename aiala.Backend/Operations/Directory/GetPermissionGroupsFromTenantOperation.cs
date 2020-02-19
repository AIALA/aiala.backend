using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Directory.ApiModels.PermissionGroups;
using xappido.Directory.Domain;
using xappido.Operations;

namespace aiala.Backend.Operations.Directory
{
    public class GetPermissionGroupsFromTenantOperation : OutputOperation<TenantPermissionGroupsAssignmentModel>
    {
        private readonly AppDbContext _dbContext;

        public GetPermissionGroupsFromTenantOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute()
        {
            var accountIds = await _dbContext.Accounts
                .Where(a => a.Tenant.Id == Context.TenantId)
                .Select(a => a.Id)
                .ToListAsync();

            var permissionGroups = await _dbContext.PermissionGroups
                .Where(e => e.Tenant == null || e.Tenant.Id == Context.TenantId)
                .Select(e => new PermissionGroupsAssignmentModel
                {
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToListAsync();

            var permissionGroupIds = permissionGroups.Select(pg => pg.Id);
            var permissionGroupAssignments = await _dbContext.PermissionGroupAssigments
                .Where(pga => accountIds.Contains(pga.AccountId) || permissionGroupIds.Contains(pga.GroupId))
                .ToListAsync();

            var returnValue = new TenantPermissionGroupsAssignmentModel();
            foreach (var accountId in accountIds)
            {
                var groups = new List<PermissionGroupsAssignmentModel>();
                foreach (var existingGroup in permissionGroups)
                {
                    var newGroup = new PermissionGroupsAssignmentModel
                    {
                        Id = existingGroup.Id,
                        Name = existingGroup.Name,
                        IsAssigned = permissionGroupAssignments.Any(e => e.Group.Id == existingGroup.Id && e.Account.Id == accountId)
                    };
                    groups.Add(newGroup);
                }
                returnValue[accountId] = groups;
            }

            return Succeeded(returnValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.Authorization.PermissionGroups;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Operations.Directory;
using aiala.Backend.Services;
using xappido.Authorization.Attributes;
using xappido.Directory.Controllers;
using xappido.Directory.Domain;
using xappido.Directory.PermissionGroups;
using xappido.Directory.Policies;
using xappido.Directory.Services;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Controllers
{
    public class UsersController : UsersController<
        AppDbContext,
        Tenant,
        Account,
        User,
        UserDetailModel,
        AppUserCreateModel,
        UserDetailModel,
        AppUserCreateModel,
        UserDetailModel,
        UserDetailModel>
    {
        private readonly IPictureHelperService _pictureHelper;
        private readonly IEnumerable<IPermissionGroupConfiguration> _permissionGroups;

        public UsersController(IOperationExecutor executor, IPictureHelperService pictureHelper, IEnumerable<IPermissionGroupConfiguration> permissionGroups) : base(executor)
        {
            _pictureHelper = pictureHelper;
            _permissionGroups = permissionGroups;
        }

        protected override void MapPropertiesToAccountOnCreate(AppUserCreateModel model, Account account)
        {
            // Directory
            account.User.Firstname = model.Firstname;
            account.User.Lastname = model.Lastname;
            account.User.Culture = model.Culture;
            account.User.Email = model.Email;

            // AIALA
            account.PhoneNumber = model.PhoneNumber;
            account.PermissionGroupAssignments = new List<PermissionGroupAssignment<Tenant, Account, User>>();
            var permissionGroupConfigTypes = new List<Type>();

            switch (model.Role)
            {
                case UserCreateRole.MobileAppUser:
                    permissionGroupConfigTypes.Add(typeof(MobileAppUserPermissionGroup));
                    break;
                case UserCreateRole.WebAppUser:
                    permissionGroupConfigTypes.Add(typeof(WebAppUserPermissionGroup));
                    break;
                case UserCreateRole.Administrator:
                    permissionGroupConfigTypes.Add(typeof(AppAdministratorPermissionGroup));
                    permissionGroupConfigTypes.Add(typeof(MobileAppUserPermissionGroup));
                    break;
                default:
                    break;
            }

            if (permissionGroupConfigTypes.Count > 0)
            {
                foreach (var configType in permissionGroupConfigTypes)
                {
                    account.PermissionGroupAssignments.Add(new PermissionGroupAssignment<Tenant, Account, User>
                    {
                        GroupId = _permissionGroups.Where(p => p.GetType() == configType).Single().Id,
                        Id = Guid.NewGuid()
                    });
                }
                model.Invite = true;
            }
            else
            {
                model.Invite = false;
            }
        }

        protected override void MapPropertiesToAccountOnUpdate(AppUserCreateModel model, Account account)
        {
            MapPropertiesToAccountOnCreate(model, account);
        }

        protected override async Task<UserDetailModel> MapCreateResult(IQueryable<Account> accounts)
        {
            return (await MapListResult(accounts)).FirstOrDefault();
        }

        protected override Task<UserDetailModel> MapDetailResult(IQueryable<Account> accounts)
        {
            return MapCreateResult(accounts);
        }

        protected override Task<UserDetailModel> MapUpdateResult(IQueryable<Account> accounts)
        {
            return MapCreateResult(accounts);
        }

        protected override async Task<List<UserDetailModel>> MapListResult(IQueryable<Account> accounts)
        {
            var tuples = await accounts.Select(account => new
            {
                account = account,
                userDetail = new UserDetailModel
                {
                    Id = account.Id,
                    Enabled = account.Enabled,
                    Firstname = account.User.Firstname,
                    Lastname = account.User.Lastname,
                    InvitationStatus = account.Invitation.Status,
                    Email = account.User.Email,
                    Culture = account.User.Culture,
                    PhoneNumber = account.PhoneNumber,
                    PermissionGroups = account.PermissionGroupAssignments.Select(pa => pa.Group.Name).ToList(),
                    PictureUrl = _pictureHelper.GetPictureUrl(account.Picture, PictureType.Profile),
                }
            }).ToListAsync();

            var result = new List<UserDetailModel>();
            foreach(var tuple in tuples)
            {
                tuple.userDetail.Picture = await _pictureHelper.MapViaOperation(tuple.account.Picture);
                result.Add(tuple.userDetail);
            }

            return result;
        }

        [HttpGet("tenant/permissiongroups")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(typeof(TenantPermissionGroupsAssignmentModel), (int)HttpStatusCode.OK)]
        [AuthorizePolicy(typeof(ManageUsersPolicy))]
        public async Task<IActionResult> GetPermissionGroupsForTenant()
        {
            var result = await Executor
                .Validate(ModelState)
                .Add<GetPermissionGroupsFromTenantOperation, List<IDirectoryAccount>>()
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

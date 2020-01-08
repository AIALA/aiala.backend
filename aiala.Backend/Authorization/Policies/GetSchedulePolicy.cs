using Microsoft.AspNetCore.Authorization;
using aiala.Backend.Authorization.Permissions;
using xappido.Authorization.Extensions;
using xappido.Authorization.Policies;

namespace aiala.Backend.Authorization.Policies
{
    public class GetSchedulePolicy : ValidUserPolicy
    {
        protected override void OnBuild(AuthorizationPolicyBuilder builder)
        {
            base.OnBuild(builder);

            builder.RequirePermission<GetSchedulePermission>();
        }
    }
}

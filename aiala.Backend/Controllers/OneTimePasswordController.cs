using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Documents;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Models.Directory;
using System;
using System.Threading;
using System.Threading.Tasks;
using xappido.Authentication.Otp.ApiModels;
using xappido.Authorization.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    public class OneTimePasswordController : xappido.Authentication.Otp.Controllers.OneTimePasswordController
    {
        public OneTimePasswordController(IOperationExecutor executor) : base(executor)
        {
        }

        [AuthorizePolicy(typeof(GetSchedulePolicy))]
        [HttpPost("day/{date}")]
        [ProducesResponseType(typeof(OneTimePasswordModel), 200)]
        public async Task<IActionResult> GenerateDayOtp([FromRoute] DateTimeOffset date)
        {
            return (await GenerateOneTimePassword(new DayOtpModel
            {
                Date = date,
                Culture = Thread.CurrentThread.CurrentCulture.ToAppCulture()
            })).CreateHttpResult();
        }
    }
}

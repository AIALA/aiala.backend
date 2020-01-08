using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Mappings.Reports;
using aiala.Backend.Models.Reports;
using aiala.Backend.Operations.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Authorize]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public ReportsController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpGet("activities")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetReportActivites()
        {
            var result = await _executor
                .Add<GetReportActivitiesOperation>()
                .Map<ReportActivitiesToDictionaryMapping, IEnumerable<ReportActivity>>(a => a)
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

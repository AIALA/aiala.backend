using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Schedule;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Mappings.Schedule;
using aiala.Backend.Models.Schedule;
using aiala.Backend.Operations;
using aiala.Backend.Operations.Schedule;
using xappido.Authorization;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    /// <summary>
    /// Controller for the schedule and its days.
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Schedule")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public ScheduleController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Get Schedule
        /// </summary>
        /// <param name="from">Inclusive date from</param>
        /// <param name="to">Inclusive date to</param>
        /// <returns>Schedule, consisting of all days in selected range</returns>
        [AuthorizePolicy(typeof(GetSchedulePolicy))]
        [HttpGet]
        [ProducesResponseType(typeof(List<DayModel>), 200)]
        public async Task<IActionResult> GetDays([FromQuery] DateTimeOffset from, [FromQuery] DateTimeOffset to)
        {
            var result = await _executor
                .Add<ValidateIsUtcStartOfDayOperation, (DateTimeOffset, object)>(() => (from, null))
                .Add<ValidateIsUtcStartOfDayOperation, (DateTimeOffset, object)>(() => (to, null))
                .Add<GetDaysOperation, GetDaysOperation.Input>(() => new GetDaysOperation.Input(from, to))
                .Add<FillDaysRangeOperation, IEnumerable<Day>, (DateTimeOffset, DateTimeOffset, IEnumerable<Day>)>(days =>
                {
                    return (from, to, days);
                })
                .Map<DayToDayModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Upsert Day
        /// </summary>
        /// <param name="day">Day to upsert</param>
        /// <param name="steamrollerMode">Steamroller mode to enable bypassing the schedule validation and steam rolling over the data. Warning: Use with caution!</param>
        /// <returns></returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpsertDay([FromBody] UpsertedDayModel day, [FromQuery] bool steamrollerMode)
        {
            _executor
                .Validate(ModelState)
                .Map<UpsertedDayModelToUpsertedDayMapping>(() => day)
                .Add<ValidateIsUtcStartOfDayOperation, UpsertedDay, (DateTimeOffset, object)>(d => (d.Date, d));

            if (!steamrollerMode)
            {
                _executor
                    .Add<ValidateScheduledTasksOperation, UpsertedDay, ScheduledTasksValidationModel>(d => new ScheduledTasksValidationModel(d, d.Tasks, d.Date));
            }

            var result = await _executor
                .Add<UpsertDayOperation, UpsertedDay, (Guid, UpsertedDay)>(d => (User.GetTenantId().Value, d))
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Activities;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Activities;
using aiala.Backend.Operations.Activities;
using aiala.Backend.Operations.Notifications;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers.Activities
{
    /// <summary>
    /// Controller to track emergency activities.
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("activities/emergency")]
    [ApiExplorerSettings(GroupName = "Activities - Emergency")]
    [AuthorizePolicy(typeof(ScheduleConsumptionPolicy))]
    public class EmergencyActivitiesController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public EmergencyActivitiesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Start emergency
        /// </summary>
        /// <remarks>
        /// Starts a new emergency with the given emergency id and sends notifications about that
        /// </remarks>
        /// <param name="emergencyId">The ID used to correlate different emergency events with the same emergency</param>
        /// <param name="metadata">The activity metadata</param>
        /// <returns></returns>
        [HttpPost("{emergencyId}/start")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> StartEmergency(Guid emergencyId, [FromBody] ActivityMetadataModel metadata)
        {
            var result = await _executor
                .Add<CreateAndValidateEmergencyActivityOperation, (Guid, ActivityType)>(() => (emergencyId, ActivityType.EmergencyStart))
                .Add<AddActivityOperation, EmergencyActivity, AddActivityOperation.Request>(a => new AddActivityOperation.Request(metadata, a))
                .Add<EnqueueEmergencyNotificationOperation, EmergencyActivity, EmergencyActivity>(a => a)
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Update emergency mood
        /// </summary>
        /// <remarks>
        /// Sets the mood of the given emergency to the given mood and sends notifications about that
        /// </remarks>
        /// <param name="emergencyId">The ID used to correlate different emergency events with the same emergency</param>
        /// <param name="model">The activity metadata and the new mood</param>
        /// <returns></returns>
        [HttpPost("{emergencyId}/mood")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateEmergencyMood(Guid emergencyId, [FromBody] UpdateEmergencyMoodModel model)
        {
            var result = await _executor
                .Add<CreateAndValidateEmergencyActivityOperation, (Guid, ActivityType)>(() => (emergencyId, ActivityType.EmergencyMood))
                .Add<AddActivityOperation, EmergencyActivity, AddActivityOperation.Request>(a =>
                    new AddActivityOperation.Request(model, a, new Dictionary<string, object>
                    {
                        [EmergencyActivity.MoodDataKey] = model.Mood
                    }))
                .Add<EnqueueEmergencyNotificationOperation, EmergencyActivity, EmergencyActivity>(a => a)
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// End emergency
        /// </summary>
        /// <remarks>
        /// Ends the emergency and sends notifications about that
        /// </remarks>
        /// <param name="emergencyId">The ID used to correlate different emergency events with the same emergency</param>
        /// <param name="metadata">The activity metadata</param>
        /// <returns></returns>
        [HttpPost("{emergencyId}/end")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EndEmergency(Guid emergencyId, [FromBody] ActivityMetadataModel metadata)
        {
            var result = await _executor
                .Add<CreateAndValidateEmergencyActivityOperation, (Guid, ActivityType)>(() => (emergencyId, ActivityType.EmergencyEnd))
                .Add<AddActivityOperation, EmergencyActivity, AddActivityOperation.Request>(a => new AddActivityOperation.Request(metadata, a))
                .Add<EnqueueEmergencyNotificationOperation, EmergencyActivity, EmergencyActivity>(a => a)
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Activities;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Activities;
using aiala.Backend.Models.Activities;
using aiala.Backend.Operations.Activities;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers.Activities
{
    /// <summary>
    /// Controller to track task activities.
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("activities/task")]
    [ApiExplorerSettings(GroupName = "Activities - Task")]
    [AuthorizePolicy(typeof(ScheduleConsumptionPolicy))]
    public class TaskActivitiesController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public TaskActivitiesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Update task expiration offset
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{taskId}/delay")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTaskExpirationOffset(Guid taskId, [FromBody] DelayTaskModel model)
        {
            var result = await _executor
                .Add<UpdateTaskExpirationOffsetOperation, (Guid taskId, TimeSpan delayedUntil)>(() => (taskId, model.DelayedUntil))
                .Add<CreateScheduledTaskActivityOperation, (Guid taskId, ActivityType type)>(() => (taskId, ActivityType.TaskDelayed))
                .Add<AddActivityOperation, ScheduledTaskActivity, AddActivityOperation.Request>(activity =>
                    new AddActivityOperation.Request(model, activity, new Dictionary<string, object>
                    {
                        [ScheduledTaskActivity.TaskDelayedUntilKey] = model.DelayedUntil
                    }))
                .Execute();

            // Return empty payload if succeeded
            return result.ResultType == OperationResultType.Succeeded ? Ok() : result.CreateHttpResult();
        }

        /// <summary>
        /// Update task feedback
        /// </summary>
        /// <param name="taskId">Task ID</param>
        /// <param name="model">Model containing the new feedback</param>
        /// <returns></returns>
        [HttpPost("{taskId}/feedback")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTaskFeedback(Guid taskId, [FromBody] UpdateTaskFeedbackModel model)
        {
            var operationsModel = new UpdateTaskFeedbackOperationsModel
            {
                TaskId = taskId,
                NewTaskFeedback = model.Feedback
            };

            var result = await _executor
                .Add<UpdateTaskFeedbackOperation, UpdateTaskFeedbackOperationsModel>(() => operationsModel)
                .Add<CreateScheduledTaskActivityOperation, (Guid taskId, ActivityType type)>(() => (taskId, ActivityType.TaskFeedback))
                .Add<AddActivityOperation, ScheduledTaskActivity, AddActivityOperation.Request>(activity =>
                    new AddActivityOperation.Request(model, activity, new Dictionary<string, object>
                    {
                        [ScheduledTaskActivity.FeedbackDataKey] = model.Feedback
                    }))
                .Execute();

            // Return empty payload if succeeded
            return result.ResultType == OperationResultType.Succeeded ? Ok() : result.CreateHttpResult();
        }
    }
}

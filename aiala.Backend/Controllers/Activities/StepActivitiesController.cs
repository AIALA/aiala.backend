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
    /// Controller to track step activities.
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("activities/step")]
    [ApiExplorerSettings(GroupName = "Activities - Step")]
    [AuthorizePolicy(typeof(ScheduleConsumptionPolicy))]
    public class StepActivitiesController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public StepActivitiesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Update step state
        /// </summary>
        /// <remarks>
        /// Will validate for validity of the state change according to the state of the day.
        /// A step can only be set done if no earlier step is undone and a step can only be set undone if no later step is done.
        /// </remarks>
        /// <param name="stepId">Step ID</param>
        /// <param name="model">Model containing the new state</param>
        /// <returns></returns>
        [HttpPost("{stepId}/state")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateStepState(Guid stepId, [FromBody] UpdateStepStateModel model)
        {
            var operationsModel = new UpdateStepStateOperationsModel
            {
                NewStepState = model.State,
                StepId = stepId
            };

            var result = await _executor
                .Add<ValidateStepStateChangeOperation, UpdateStepStateOperationsModel>(() => operationsModel)
                .Add<UpdateStepStateOperation, UpdateStepStateOperationsModel, UpdateStepStateOperationsModel>(m => m)
                .Add<CreateScheduledStepActivityOperation, (Guid taskId, ActivityType type)>(() => (stepId, ActivityType.StepState))
                .Add<AddActivityOperation, ScheduledStepActivity, AddActivityOperation.Request>(activity =>
                    new AddActivityOperation.Request(model, activity, new Dictionary<string, object>
                    {
                        [ScheduledStepActivity.StateDataKey] = model.State
                    }))
                .Execute();

            // Return empty payload if succeeded
            return result.ResultType == OperationResultType.Succeeded ? Ok() : result.CreateHttpResult();
        }
    }
}

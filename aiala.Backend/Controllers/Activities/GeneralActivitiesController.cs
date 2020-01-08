using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Activities;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Activities;
using aiala.Backend.Operations.Activities;
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
    [Route("activities/general")]
    [ApiExplorerSettings(GroupName = "Activities - General")]
    [AuthorizePolicy(typeof(ScheduleConsumptionPolicy))]
    public class GeneralActivitiesController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public GeneralActivitiesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Log General Activity
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpPost("{activityType}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LogGeneralActivtiy(GeneralApiActivityType? activityType, [FromBody] GeneralActivityModel metadata)
        {
            var activityData = metadata.ActivityData?.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);

            var result = await _executor
                .Add<CreateGeneralActivityOperation, ActivityType>(() => (ActivityType) activityType.Value)
                .Add<AddActivityOperation, GeneralActivity, AddActivityOperation.Request>(a => new AddActivityOperation.Request(metadata, a, activityData))
                .Execute();

            // Return empty payload if succeeded
            return result.ResultType == OperationResultType.Succeeded ? Ok() : result.CreateHttpResult();
        }
    }
}

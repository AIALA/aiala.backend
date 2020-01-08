using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.ApiModels.Tasks;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Tasks;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Mappings.Tasks;
using aiala.Backend.Operations.Pictures;
using aiala.Backend.Operations.Tasks;
using xappido.Authorization;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Controllers
{
    /// <summary>
    /// Controller for tasks, which can later be applied to the schedule
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private const string ContainerName = "taskpictures";

        private readonly IOperationExecutor _executor;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IOperationExecutor executor, ILogger<TasksController> logger)
        {
            _executor = executor;
            _logger = logger;
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <remarks>Gets all tasks for the current group.</remarks>
        /// <returns>All Tasks</returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AppTaskModel>))]
        public async Task<IActionResult> Get()
        {
            var temp = User.Claims.ToList();
            var result = await _executor
                .Add<GetTasksOperation>()
                .Map<AppTaskToAppTaskModelMapping, List<AppTask>>(last => last)
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Upsert Task
        /// </summary>
        /// <param name="task">The task to either add or updated</param>
        /// <returns>The updated Task</returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AppTaskModel))]
        public async Task<IActionResult> UpsertTask([FromBody] AppTaskModel task)
        {
            var result = await _executor
                .Validate(ModelState)
                .Map<AppTaskToAppTaskModelMapping>(() => task)
                .Add<UpsertTaskOperation, AppTask, (Guid, AppTask)>(last => (User.GetAccountId().Value, last))
                .Map<AppTaskToAppTaskModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpDelete("{taskId}")]
        [ProducesResponseType(200, Type = typeof(List<AppTaskModel>))]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var result = await _executor
                .Add<ValidateTaskDeletionOperation, Guid>(() => taskId)
                .Add<DeleteTaskOperation, Guid>(last => last)
                .Add<GetTasksOperation>()
                .Map<AppTaskToAppTaskModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

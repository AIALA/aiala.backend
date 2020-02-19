using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Templates;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Templates;
using aiala.Backend.Mappings.Templates;
using aiala.Backend.Models.Schedule;
using aiala.Backend.Operations.Schedule;
using aiala.Backend.Operations.Templates;
using xappido.Authorization;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    /// <summary>
    /// Controller for scheduling templates
    /// </summary>
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Templates")]
    [Authorize]
    public class TemplatesController : ControllerBase
    {
        private readonly IOperationExecutor _executor;

        public TemplatesController(IOperationExecutor executor)
        {
            _executor = executor;
        }


        /// <summary>
        /// Get day templates
        /// </summary>
        /// <returns>List of day templates for tenant</returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpGet]
        [ProducesResponseType(typeof(List<DayTemplateModel>), 200)]
        public async Task<IActionResult> GetTemplates()
        {
            var result = await _executor
                .Add<GetDayTemplatesOperation, Guid>(() => User.GetTenantId().Value)
                .Map<DayTemplateModelToDayTemplateMapping, List<DayTemplate>>(t => t)
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Create day template
        /// </summary>
        /// <remarks>
        /// This will not update day templates, only create them.
        /// </remarks>
        /// <param name="template">Template to create</param>
        /// <returns></returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDayTemplate([FromBody] DayTemplateModel template)
        {
            var result = await _executor
                .Validate(ModelState)
                .Map<DayTemplateModelToDayTemplateMapping>(() => template)
                .Add<ValidateTasksNotDeletedOperation, DayTemplate, DayTemplate>(last => last)
                .Add<ValidateScheduledTasksOperation, DayTemplate, ScheduledTasksValidationModel>(t => new ScheduledTasksValidationModel(t, t.Tasks, null))
                .Add<CreateDayTemplateOperation, DayTemplate, (Guid, DayTemplate)>(t => (User.GetTenantId().Value, t))
                .Execute();

            return result.CreateHttpResult();
        }

        // TODO: Change this to /{id}
        /// <summary>
        /// Delete day template
        /// </summary>
        /// <remarks>
        /// Permanently deletes a day template.
        /// </remarks>
        /// <param name="id">ID to delete</param>
        /// <returns></returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteDayTemplate(Guid id)
        {
            var result = await _executor
                .Add<DeleteDayTemplateOperation, (Guid, Guid)>(() => (User.GetTenantId().Value, id))
                .Execute();

            return result.CreateHttpResult();
        }
    }
}

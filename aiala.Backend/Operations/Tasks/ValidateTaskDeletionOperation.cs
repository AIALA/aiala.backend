using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Resources;
using xappido.Operations;

namespace aiala.Backend.Operations.Tasks
{
    public class ValidateTaskDeletionOperation : InputOutputOperation<Guid, Guid>
    {
        private readonly AppDbContext _dbContext;

        public ValidateTaskDeletionOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Guid input)
        {
            var dayTemplateNames = await _dbContext.ScheduledTaskTemplates
                .Where(t => t.TaskId == input)
                .Select(t => t.DayTemplate)
                .Distinct()
                .ToListAsync();

            if (dayTemplateNames.Any())
            {
                var taskName = await _dbContext.Tasks.Where(t => t.Id == input).Select(t => t.Name).FirstOrDefaultAsync();
                var message = string.Format(ValidationMessages.CannotDeleteTaskBecauseTemplates, taskName);
                message += message.TrimEnd() + " " + string.Join(", ", dayTemplateNames);
                return Invalid(message);
            }

            return Succeeded(input);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Templates;
using aiala.Backend.Resources;
using xappido.Operations;

namespace aiala.Backend.Operations.Templates
{
    public class ValidateTasksNotDeletedOperation : InputOutputOperation<DayTemplate, DayTemplate>
    {
        private readonly AppDbContext _dbContext;

        public ValidateTasksNotDeletedOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(DayTemplate input)
        {
            var taskIds = input.Tasks.Select(t => t.TaskId).ToArray();
            var deletedTaskNames = await _dbContext.Tasks
                .IgnoreQueryFilters()
                .Where(t => taskIds.Contains(t.Id) && t.IsDeleted)
                .Select(t => t.Name)
                .ToListAsync();

            if (deletedTaskNames.Any())
            {
                var message = ValidationMessages.CannotCreateTemplateTaskDeleted;
                message = message.TrimEnd() + " " + string.Join(", ", deletedTaskNames);
                return Invalid(message);
            }

            return Succeeded(input);
        }
    }
}

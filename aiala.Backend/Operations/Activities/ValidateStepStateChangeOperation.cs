using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class ValidateStepStateChangeOperation : InputOutputOperation<UpdateStepStateOperationsModel, UpdateStepStateOperationsModel>
    {
        private readonly AppDbContext _dbContext;

        public ValidateStepStateChangeOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(UpdateStepStateOperationsModel input)
        {
            var step = await _dbContext.ScheduledSteps
                .Include(s => s.Task)
                    .ThenInclude(t => t.Day)
                .FirstOrDefaultAsync(s => s.Id == input.StepId && s.Task.Day.Tenant.Id == Context.TenantId);

            if (step == null)
            {
                return NotFound("Step not found");
            }

            var task = step.Task;
            var day = task.Day;

            if (input.NewStepState == StepState.Done)
            {
                var earlierUndoneSteps = day.Tasks
                    .Where(t => t.End <= task.Start)
                    .SelectMany(t => t.Steps)
                    .Concat(task.Steps.Where(s => s.Order < step.Order))
                    .Where(s => s.State != StepState.Done);

                if (earlierUndoneSteps.Any())
                {
                    return Invalid("Earlier step not done yet. Mark earlier step as done first.");
                }
            }
            else
            {
                var laterDoneSteps = day.Tasks
                    .Where(t => t.Start >= task.End)
                    .SelectMany(t => t.Steps)
                    .Concat(task.Steps.Where(s => s.Order > step.Order))
                    .Where(s => s.State != StepState.Undone);

                if (laterDoneSteps.Any())
                {
                    return Invalid("Later step done yet. Mark later step as undone first.");
                }
            }

            return Succeeded(input);
        }
    }
}

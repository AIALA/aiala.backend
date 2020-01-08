using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Models.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class UpdateTaskFeedbackOperation : InputOperation<UpdateTaskFeedbackOperationsModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateTaskFeedbackOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(UpdateTaskFeedbackOperationsModel input)
        {
            var task = await _dbContext.ScheduledTasks.FirstOrDefaultAsync(t => t.Id == input.TaskId && t.Day.Tenant.Id == Context.TenantId);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            task.Feedback = input.NewTaskFeedback;

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

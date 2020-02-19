using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class CreateScheduledTaskActivityOperation : InputOutputOperation<(Guid taskId, ActivityType type), ScheduledTaskActivity>
    {
        private readonly AppDbContext _dbContext;

        public CreateScheduledTaskActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid taskId, ActivityType type) input)
        {
            if (!ActivityTypeRanges.Task.Contains(input.type))
            {
                return Invalid($"Activity type {Enum.GetName(typeof(ActivityType), input.type)} is not a task activity type");
            }

            var task = await _dbContext.ScheduledTasks.FirstOrDefaultAsync(t => t.Id == input.taskId && t.Day.Tenant.Id == Context.TenantId);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            return Succeeded(new ScheduledTaskActivity
            {
                Type = input.type,
                Task = task,
                Tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == Context.TenantId)
            });
        }
    }
}

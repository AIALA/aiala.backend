using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class UpdateTaskExpirationOffsetOperation : InputOperation<(Guid taskId, TimeSpan delayedUntil)>
    {
        private readonly AppDbContext _dbContext;

        public UpdateTaskExpirationOffsetOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid taskId, TimeSpan delayedUntil) input)
        {
            var tenantId = Context.User.GetTenantId().Value;
            var task = await _dbContext.ScheduledTasks.FirstOrDefaultAsync(t => t.Id == input.taskId && t.Day.Tenant.Id == tenantId);
            if (task == null)
            {
                return NotFound();
            }

            task.ExpirationOffset = input.delayedUntil.Subtract(task.End);
            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

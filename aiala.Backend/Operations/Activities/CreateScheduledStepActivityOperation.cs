using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class CreateScheduledStepActivityOperation : InputOutputOperation<(Guid stepId, ActivityType type), ScheduledStepActivity>
    {
        private readonly AppDbContext _dbContext;

        public CreateScheduledStepActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid stepId, ActivityType type) input)
        {
            if (!ActivityTypeRanges.Step.Contains(input.type))
            {
                return Invalid($"Activity type {Enum.GetName(typeof(ActivityType), input.type)} is not a step activity type");
            }
            
            var step = await _dbContext.ScheduledSteps.FirstOrDefaultAsync(s => s.Id == input.stepId && s.Task.Day.Tenant.Id == Context.TenantId);

            if (step == null)
            {
                return NotFound("Step not found");
            }

            return Succeeded(new ScheduledStepActivity
            {
                Type = input.type,
                Step = step,
                Tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == Context.TenantId)
            });
        }
    }
}

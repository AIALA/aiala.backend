using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Models.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class UpdateStepStateOperation : InputOperation<UpdateStepStateOperationsModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateStepStateOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(UpdateStepStateOperationsModel input)
        {
            var step = await _dbContext.ScheduledSteps.FirstOrDefaultAsync(s => s.Id == input.StepId && s.Task.Day.Tenant.Id == Context.TenantId);
            
            if (step == null)
            {
                return NotFound("Step not found");
            }

            step.State = input.NewStepState;

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

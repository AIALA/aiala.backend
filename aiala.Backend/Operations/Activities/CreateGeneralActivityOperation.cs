using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class CreateGeneralActivityOperation : InputOutputOperation<ActivityType, GeneralActivity>
    {
        private readonly AppDbContext _dbContext;

        public CreateGeneralActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(ActivityType input)
        {
            if (!ActivityTypeRanges.General.Contains(input))
            {
                return Invalid($"Activity type {Enum.GetName(typeof(ActivityType), input)} is not a general activity type");
            }

            return Succeeded(new GeneralActivity
            {
                Type = input,
                Tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == Context.TenantId)
            });
        }
    }
}

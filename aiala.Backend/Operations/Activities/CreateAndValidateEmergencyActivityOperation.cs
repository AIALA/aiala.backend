using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class CreateAndValidateEmergencyActivityOperation : InputOutputOperation<(Guid emergencyId, ActivityType type), EmergencyActivity>
    {
        private readonly AppDbContext _dbContext;

        public CreateAndValidateEmergencyActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid emergencyId, ActivityType type) input)
        {
            if (!ActivityTypeRanges.Emergency.Contains(input.type))
            {
                return Invalid($"Activity type {Enum.GetName(typeof(ActivityType), input.type)} is not a emergency activity type");
            }

            switch (input.type)
            {
                case ActivityType.EmergencyStart:
                    if (await _dbContext.EmergencyActivities.AnyAsync(a => a.EmergencyId == input.emergencyId))
                    {
                        return Invalid("Emergency already started");
                    }
                    break;
                case ActivityType.EmergencyMood:
                case ActivityType.EmergencyEnd:
                    if (!await _dbContext.EmergencyActivities.AnyAsync(a => a.EmergencyId == input.emergencyId && a.Type == ActivityType.EmergencyStart))
                    {
                        return Invalid("Emergency not started yet");
                    }
                    if (await _dbContext.EmergencyActivities.AnyAsync(a => a.EmergencyId == input.emergencyId && a.Type == ActivityType.EmergencyEnd))
                    {
                        return Invalid("Emergency already ended");
                    }
                    break;
            }

            if (await _dbContext.EmergencyActivities.AnyAsync(a => a.EmergencyId == input.emergencyId && a.Tenant.Id != Context.TenantId))
            {
                return NotAllowed("Emergency is of a different tenant");
            }

            return Succeeded(new EmergencyActivity
            {
                EmergencyId = input.emergencyId,
                Type = input.type,
                Tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == Context.TenantId)
            });
        }
    }
}

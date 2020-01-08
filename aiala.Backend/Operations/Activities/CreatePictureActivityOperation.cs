using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using System;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class CreatePictureActivityOperation : InputOutputOperation<(Guid pictureId, ActivityType type), PictureActivity>
    {
        private readonly AppDbContext _dbContext;

        public CreatePictureActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid pictureId, ActivityType type) input)
        {
            if (!ActivityTypeRanges.Picture.Contains(input.type))
            {
                return Invalid($"Activity type {Enum.GetName(typeof(ActivityType), input.type)} is not a picture activity type");
            }

            var picture = await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == input.pictureId && p.Tenant.Id == Context.TenantId);

            if (picture == null)
            {
                return NotFound("Picture not found");
            }

            return Succeeded(new PictureActivity
            {
                Type = input.type,
                Picture = picture,
                Tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == Context.TenantId)
            });
        }
    }
}

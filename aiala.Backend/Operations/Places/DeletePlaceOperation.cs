using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Resources;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Places
{
    public class DeletePlaceOperation : InputOperation<Guid>
    {
        private readonly AppDbContext _dbContext;

        public DeletePlaceOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Guid input)
        {
            var tenantId = Context.User.GetTenantId().Value;
            var place = await _dbContext.Places.FirstOrDefaultAsync(p => p.Id == input && p.Tenant.Id == tenantId);

            if (place == null)
            {
                return NotFound(Messages.PlaceNotFound);
            }

            place.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Places;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Places
{
    public class UpsertPlaceOperation : InputOutputOperation<Place, Place>
    {
        private readonly AppDbContext _dbContext;

        public UpsertPlaceOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Place input)
        {
            var tenantId = Context.User.GetTenantId().Value;

            var existing = await _dbContext.Places
                .Include(p => p.Picture)
                // Just in case a deleted entity with the same ID exists
                .IgnoreQueryFilters()
                .Where(p => p.Tenant.Id == tenantId && p.Id == input.Id)
                .FirstOrDefaultAsync();

            if (existing == null)
            {
                var tenant = await _dbContext.Tenants.SingleOrDefaultAsync(t => t.Id == tenantId);
                input.Tenant = tenant;
                _dbContext.Places.Add(input);
            }
            else
            {
                existing.Name = input.Name;
                existing.Picture = input.Picture;
                existing.Latitude = input.Latitude;
                existing.Longitude = input.Longitude;
                existing.IsDeleted = false;
            }

            await _dbContext.SaveChangesAsync();

            return Succeeded(input);
        }
    }
}

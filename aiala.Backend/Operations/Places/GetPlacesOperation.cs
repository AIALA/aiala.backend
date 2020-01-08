using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Places;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Places
{
    public class GetPlacesOperation : OutputOperation<List<Place>>
    {
        private readonly AppDbContext _dbContext;

        public GetPlacesOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute()
        {
            var tenantId = Context.User.GetTenantId().Value;
            var places = await _dbContext.Places
                .Include(p => p.Picture)
                .Where(p => p.Tenant.Id == tenantId)
                .ToListAsync();
            return Succeeded(places);
        }
    }
}

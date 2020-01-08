using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Pictures
{
    public class GetPicturesOperation : InputOutputOperation<PictureType, List<Picture>>
    {
        private readonly AppDbContext _dbContext;

        public GetPicturesOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(PictureType input)
        {
            var pictures = await _dbContext.Pictures
                .Include(p => p.AiMetadata)
                    .ThenInclude(p => p.Tags)
                .Where(p => p.PictureType == input && p.Tenant.Id == Context.TenantId)
                .ToListAsync();

            return Succeeded(pictures);
        }
    }
}

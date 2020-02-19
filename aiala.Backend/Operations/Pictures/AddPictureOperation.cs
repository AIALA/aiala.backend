using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using System;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Pictures
{
    public class AddPictureOperation : InputOutputOperation<(Guid pictureId, PictureType pictureType, string storageDirectLink), Picture>
    {
        private readonly AppDbContext _dbContext;

        public AddPictureOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid pictureId, PictureType pictureType, string storageDirectLink) input)
        {
            var existing = await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == input.pictureId);

            if (existing != null)
            {
                return Succeeded(existing);
            }

            var picture = new Picture
            {
                Id = input.pictureId,
                PictureType = input.pictureType,
                CreatedAt = DateTimeOffset.UtcNow,
                StorageDirectLink = input.storageDirectLink,
                Tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == Context.TenantId)
            };
            _dbContext.Pictures.Add(picture);
            await _dbContext.SaveChangesAsync();

            return Succeeded(picture);
        }
    }
}

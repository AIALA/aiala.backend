using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Pictures
{
    public class DeletePictureOperation : InputOperation<Guid>
    {
        private readonly AppDbContext _dbContext;

        public DeletePictureOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Guid input)
        {
            var picture = await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == input && p.Tenant.Id == Context.TenantId);
            if (picture == null)
            {
                return NotFound();
            }

            await Execute<DeleteStoragePictureOperation>((picture.PictureType.ToString(), input));

            _dbContext.PictureActivities.RemoveRange(_dbContext.PictureActivities.Where(pt => pt.Picture.Id == input));
            _dbContext.AiPictureTags.RemoveRange(_dbContext.AiPictureTags.Where(pt => pt.Picture.Id == input));
            _dbContext.AiPictureMetadatas.RemoveRange(_dbContext.AiPictureMetadatas.Where(pt => pt.Picture.Id == input));
            _dbContext.Pictures.Remove(picture);
            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

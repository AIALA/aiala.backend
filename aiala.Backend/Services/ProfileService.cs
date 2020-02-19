using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Directory.Services;

namespace aiala.Backend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPictureHelperService _pictureHelper;

        public ProfileService(AppDbContext dbContext, IPictureHelperService pictureHelper)
        {
            _dbContext = dbContext;
            _pictureHelper = pictureHelper;
        }

        public string GetPictureUrl(Guid accountId)
        {
            return Task.Run(async () =>
            {
                var account = await _dbContext.Accounts.Include(a => a.Picture).FirstAsync(a => a.Id == accountId);
                return _pictureHelper.GetPictureUrl(account.Picture, PictureType.Profile);
            }).Result;
        }
    }
}

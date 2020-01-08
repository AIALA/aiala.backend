using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Directory
{
    public class SetProfilePictureOperation : InputOutputOperation<(Picture picture, Guid accountId), Picture>
    {
        private readonly AppDbContext _dbContext;

        public SetProfilePictureOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Picture picture, Guid accountId) input)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == input.accountId);

            if (account == null)
            {
                return Failed("Account not found");
            }

            account.Picture = input.picture;
            await _dbContext.SaveChangesAsync();

            return Succeeded(input.picture);
        }
    }
}

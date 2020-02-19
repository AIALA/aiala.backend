using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using xappido.Operations;

namespace aiala.Backend.Operations.Tasks
{
    public class DeleteTaskOperation : InputOperation<Guid>
    {
        private readonly AppDbContext _dbContext;

        public DeleteTaskOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Guid input)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == input);

            if (task == null)
            {
                return NotFound();
            }

            task.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

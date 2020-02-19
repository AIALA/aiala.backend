using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Tasks
{
    public class GetTasksOperation : OutputOperation<IEnumerable<AppTask>>
    {
        private readonly AppDbContext _dbContext;

        public GetTasksOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute()
        {
            var tasks = await _dbContext.Tasks
                .Include(t => t.Tenant)
                .Include(t => t.Steps)
                .Include(t => t.Place)
                .Include(t => t.EmergencyContact1)
                .Include(t => t.EmergencyContact2)
                .Include(t => t.Picture)
                .Where(t => t.Tenant.Id == Context.TenantId)
                .ToListAsync();
            
            foreach(var task in tasks)
            {
                task.Steps = task.Steps.OrderBy(s => s.Order).ToList();
            }

            return Succeeded(tasks);
        }
    }
}

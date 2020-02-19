using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using xappido.Operations;

namespace aiala.Backend.Operations.Templates
{
    public class DeleteDayTemplateOperation : InputOperation<(Guid tenantId, Guid templateId)>
    {
        private readonly AppDbContext _dbContext;

        public DeleteDayTemplateOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid tenantId, Guid templateId) input)
        {
            var template = await _dbContext.DayTemplates
                .Include(t => t.Tenant)
                .Include(t => t.Tasks)
                .Where(t => t.Id == input.templateId && t.Tenant.Id == input.tenantId)
                .FirstOrDefaultAsync();

            if (template == null)
            {
                return NotFound();
            }

            _dbContext.ScheduledTaskTemplates.RemoveRange(template.Tasks);
            _dbContext.DayTemplates.Remove(template);

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

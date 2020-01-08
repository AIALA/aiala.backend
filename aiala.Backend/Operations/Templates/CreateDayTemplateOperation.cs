using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Templates;
using xappido.Operations;

namespace aiala.Backend.Operations.Templates
{
    public class CreateDayTemplateOperation : InputOperation<(Guid tenantId, DayTemplate template)>
    {
        private readonly AppDbContext _dbContext;

        public CreateDayTemplateOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid tenantId, DayTemplate template) input)
        {
            input.template.Tenant = await _dbContext.Tenants.SingleAsync(t => t.Id == input.tenantId);
            _dbContext.DayTemplates.Add(input.template);

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }
    }
}

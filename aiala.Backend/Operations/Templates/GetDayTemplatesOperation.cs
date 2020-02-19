using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Templates;
using xappido.Operations;

namespace aiala.Backend.Operations.Templates
{
    public class GetDayTemplatesOperation : InputOutputOperation<Guid, List<DayTemplate>>
    {
        private readonly AppDbContext _dbContext;

        public GetDayTemplatesOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Guid input)
        {
            var templates = await _dbContext.DayTemplates
                .Include(t => t.Tenant)
                .Include(t => t.Tasks)
                .ThenInclude(t => t.Task)
                .Where(t => t.Tenant.Id == input)
                .ToListAsync();

            return Succeeded(templates);
        }
    }
}

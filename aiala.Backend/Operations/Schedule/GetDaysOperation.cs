using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Schedule;
using xappido.Operations;

namespace aiala.Backend.Operations.Schedule
{
    public class GetDaysOperation : InputOutputOperation<GetDaysOperation.Input, IEnumerable<Day>>
    {
        private readonly AppDbContext _dbContext;

        public GetDaysOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Input input)
        {
            var days = await _dbContext.Days
                .IgnoreQueryFilters()
                // Tenant
                .Include(d => d.Tenant)

                // Task => Step
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.Steps)

                // Task => Non-Scheduled Task
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.Task)

                // Task => Picture
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.Picture)

                // Task => Place => Picture
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.Place)
                        .ThenInclude(t => t.Picture)

                // Task => EmergencyContacts
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.EmergencyContact1)
                        .ThenInclude(t => t.User)
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.EmergencyContact2)
                        .ThenInclude(t => t.User)

                .Where(d => (input.TenantId ?? Context.TenantId) == d.Tenant.Id)
                .Where(d => d.Date >= input.From && d.Date <= input.To)
                .ToListAsync();

            foreach(var day in days)
            {
                day.Tasks = day.Tasks.OrderBy(t => t.Start).ToList();
                foreach(var task in day.Tasks)
                {
                    task.Steps = task.Steps.OrderBy(s => s.Order).ToList();
                }
            }

            return Succeeded(days);
        }

        public class Input
        {
            public Input(DateTimeOffset from, DateTimeOffset to, Guid? tenantId = null)
            {
                From = from;
                To = to;
                TenantId = tenantId;
            }

            public DateTimeOffset From { get; set; }

            public DateTimeOffset To { get; set; }

            public Guid? TenantId { get; set; }
        }
    }
}

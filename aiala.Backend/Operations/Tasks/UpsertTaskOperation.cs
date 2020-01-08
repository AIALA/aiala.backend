using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Tasks
{
    public class UpsertTaskOperation : InputOutputOperation<(Guid memberId, AppTask task), AppTask>
    {
        private readonly AppDbContext _dbContext;

        public UpsertTaskOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid memberId, AppTask task) input)
        {
            AppTask existingTask = null;
            if (input.task.Id == Guid.Empty)
            {
                input.task.Id = Guid.NewGuid();
            }
            else
            {
                existingTask = await _dbContext.Tasks
                    .Include(t => t.Steps)
                    .Include(t => t.Picture)
                    .Include(t => t.Place)
                    .Include(t => t.EmergencyContact1)
                    .Include(t => t.EmergencyContact2)
                    .FirstOrDefaultAsync(t => t.Id == input.task.Id);
            }
            foreach(var step in input.task.Steps)
            {
                if (step.Id == Guid.Empty)
                {
                    step.Id = Guid.NewGuid();
                }
            }

            var member = await _dbContext.Accounts
                .Include(g => g.Tenant)
                .SingleAsync(m => m.Id == input.memberId);

            input.task.LastModified = DateTimeOffset.UtcNow;
            input.task.Author = member;
            input.task.Tenant = member.Tenant;

            if (existingTask == null)
            {
                _dbContext.Tasks.Add(input.task);
            }
            else
            {
                existingTask.Author = input.task.Author;
                existingTask.Duration = input.task.Duration;
                existingTask.Tenant = input.task.Tenant;
                existingTask.LastModified = input.task.LastModified;
                existingTask.Place = input.task.Place;
                existingTask.Name = input.task.Name;
                existingTask.Picture = input.task.Picture;
                existingTask.UseTaskContacts = input.task.UseTaskContacts;
                existingTask.EmergencyContact1 = input.task.EmergencyContact1;
                existingTask.EmergencyContact2 = input.task.EmergencyContact2;
                existingTask.FreeFormPlace = input.task.FreeFormPlace;

                if (existingTask.Place != null)
                {
                    existingTask.FreeFormPlace = null;
                }

                // Apply updates to existing steps, filter existing steps by still existing
                existingTask.Steps = existingTask.Steps
                    .Select(existingStep =>
                    {
                        var inputStep = input.task.Steps.FirstOrDefault(step => step.Id == existingStep.Id);
                        return (existing: existingStep, inputStep);
                    })
                    .Where(((Step existingStep, Step inputStep) tuple) => tuple.inputStep != null)
                    .Select(((Step existingStep, Step inputStep) tuple) =>
                    {
                        tuple.existingStep.Order = tuple.inputStep.Order;
                        tuple.existingStep.Text = tuple.inputStep.Text;
                        return tuple.existingStep;
                    })
                    .ToList();

                // Add new steps
                foreach(var inputStep in input.task.Steps)
                {
                    if(existingTask.Steps.All(s => s.Id != inputStep.Id))
                    {
                        existingTask.Steps.Add(inputStep);
                    }
                }
            }

            await _dbContext.SaveChangesAsync(Context.CancellationToken);

            return Succeeded(existingTask ?? input.task);
        }
    }
}

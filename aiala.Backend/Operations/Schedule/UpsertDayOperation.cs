using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Schedule;
using xappido.Operations;

namespace aiala.Backend.Operations.Schedule
{
    public class UpsertDayOperation : InputOperation<(Guid id, UpsertedDay model)>
    {
        private readonly AppDbContext _dbContext;

        public UpsertDayOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute((Guid id, UpsertedDay model) input)
        {
            var existing = await _dbContext.Days
                .Include(d => d.Tenant)
                .Include(d => d.Tasks)
                    .ThenInclude(t => t.Steps)
                .FirstOrDefaultAsync(d => d.Id == input.model.Id && d.Tenant.Id == Context.TenantId);

            if (existing == null)
            {
                var day = new Day
                {
                    Id = input.model.Id,
                    Date = input.model.Date,
                    Name = input.model.Name,
                    Tasks = new List<ScheduledTask>()
                };
                day.Tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == input.id);
                
                foreach(var addedTask in input.model.Tasks)
                {
                    var scheduledTask = await CreateTask(addedTask);
                    day.Tasks.Add(scheduledTask);
                }

                _dbContext.Days.Add(day);
            }
            else
            {
                existing.Name = input.model.Name;

                var tasks = existing.Tasks.ToList();

                // Remove old tasks and their steps
                var tasksToRemove = tasks.Where(et => input.model.Tasks.All(nt => nt.Id != et.Id));
                var taskIdsToRemove = tasksToRemove.Select(t => t.Id);
                var stepsToRemove = tasksToRemove.SelectMany(t => t.Steps);
                var stepIdsToRemove = stepsToRemove.Select(s => s.Id);

                //-- Remove related activities
                _dbContext.ScheduledStepActivities.RemoveRange(
                    _dbContext.ScheduledStepActivities.Where(ssa => stepIdsToRemove.Contains(ssa.Step.Id))
                );
                _dbContext.ScheduledTaskActivities.RemoveRange(
                    _dbContext.ScheduledTaskActivities.Where(ssa => taskIdsToRemove.Contains(ssa.Task.Id))
                );
                _dbContext.Activities.RemoveRange(
                    _dbContext.Activities.Where(ssa => stepIdsToRemove.Contains(ssa.ActiveStep.Id) || taskIdsToRemove.Contains(ssa.ActiveTask.Id))
                );

                //-- Remove entities
                _dbContext.ScheduledSteps.RemoveRange(stepsToRemove);
                _dbContext.ScheduledTasks.RemoveRange(tasksToRemove);
                tasks.RemoveAll(et => tasksToRemove.Any(ttr => ttr == et));

                // Update existing tasks
                foreach(var existingTask in tasks)
                {
                    var newTask = input.model.Tasks.Single(t => t.Id == existingTask.Id);
                    existingTask.Start = newTask.Start;
                    existingTask.End = newTask.End;
                }

                // Add new tasks
                var addedTasks = input.model.Tasks.Where(nt => tasks.All(et => et.Id != nt.Id)).ToList();
                foreach(var addedTask in addedTasks)
                {
                    var scheduledTask = await CreateTask(addedTask);
                    existing.Tasks.Add(scheduledTask);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Succeeded();
        }

        private async Task<ScheduledTask> CreateTask(UpsertedDayTask addedTask)
        {
            var task = await _dbContext.Tasks
                .IgnoreQueryFilters()
                .Include(t => t.Steps)
                .Include(t => t.Place)
                .Include(t => t.EmergencyContact1)
                .Include(t => t.EmergencyContact2)
                .Include(t => t.Picture)
                .SingleAsync(t => t.Id == addedTask.TaskId);

            var scheduledTask = new ScheduledTask
            {
                Id = Guid.NewGuid(),
                DefaultDuration = task.Duration,
                Name = task.Name,
                Picture = task.Picture,
                Task = task,
                Start = addedTask.Start,
                End = addedTask.End,
                EmergencyContact1 = task.EmergencyContact1,
                EmergencyContact2 = task.EmergencyContact2,
                UseTaskContacts = task.UseTaskContacts,
                Steps = task.Steps.Select(s => new ScheduledStep
                {
                    Id = Guid.NewGuid(),
                    Order = s.Order,
                    Text = s.Text
                }).ToList()
            };

            if (task.Place != null)
            {
                scheduledTask.Place = new ScheduledPlace
                {
                    Id = Guid.NewGuid(),
                    Name = task.Place.Name,
                    Picture = task.Place.Picture,
                    Latitude = task.Place.Latitude,
                    Longitude = task.Place.Longitude,
                    Place = task.Place
                };
            }
            else
            {
                scheduledTask.FreeFormPlace = task.FreeFormPlace;
            }

            return scheduledTask;
        }
    }
}

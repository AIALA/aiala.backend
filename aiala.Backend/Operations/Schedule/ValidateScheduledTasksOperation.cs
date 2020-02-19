using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Directory;
using aiala.Backend.Models.Schedule;
using aiala.Backend.Resources;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Operations.Schedule
{
    public class ValidateScheduledTasksOperation : InputOutputOperation<ScheduledTasksValidationModel, object>
    {
        private readonly ISettingsService _settingsService;

        public ValidateScheduledTasksOperation(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        protected override async Task<IOperationResult> Execute(ScheduledTasksValidationModel input)
        {
            var tasks = input.Tasks.OrderBy(t => t.Start).ToList();
            var validationResults = new List<ValidationResult>();
            if (tasks != null)
            {
                var startEndInvertedTasks = tasks.Where(t => t.End <= t.Start).ToList();
                if (startEndInvertedTasks.Any())
                {
                    foreach(var task in startEndInvertedTasks)
                    {
                        validationResults.Add(new ValidationResult(ValidationMessages.EndMustBeAfterStart, ToControlName(tasks.IndexOf(task))));
                    }
                }

                for (var i = 0; i < tasks.Count - 1; i++)
                {
                    if (tasks[i].End > tasks[i + 1].Start)
                    {
                        validationResults.Add(new ValidationResult(ValidationMessages.TaskOverlapsWithPrevious, ToControlName(i + 1)));
                    }
                }

                if (input.DayDate.HasValue)
                {
                    var nowUtc = DateTimeOffset.UtcNow;
                    var offset = (input.DayDate - new DateTimeOffset(nowUtc.Year, nowUtc.Month, nowUtc.Day, 0, 0, 0, TimeSpan.Zero)).Value.TotalMilliseconds;

                    if (offset < 0)
                    {
                        validationResults.Add(new ValidationResult(ValidationMessages.TasksInPastEdited));
                    }

                    if (offset == 0)
                    {
                        var tenantSettings = await _settingsService.Load<TenantSettings>(Context.TenantId.Value);
                        var localTime = TimeZoneInfo.ConvertTimeFromUtc(
                            DateTime.UtcNow,
                            TimeZoneInfo.FindSystemTimeZoneById(tenantSettings.TimeZoneId));
                        var earliestEditableTime = localTime.TimeOfDay.Add(TimeSpan.FromMinutes(5));

                        var existingDayResult = await Execute<GetDaysOperation>(new GetDaysOperation.Input(input.DayDate.Value, input.DayDate.Value));
                        Day existingDay = null;
                        if (existingDayResult.ResultType == OperationResultType.Succeeded)
                        {
                            existingDay = existingDayResult.GetResult<List<Day>>().FirstOrDefault();
                        }

                        if (existingDay != null)
                        {
                            foreach (var deleted in existingDay.Tasks.Where(existing => tasks.All(t => t.Id != existing.Id)))
                            {
                                if (deleted.Start < earliestEditableTime)
                                {
                                    validationResults.Add(new ValidationResult($"{deleted.Name}: {ValidationMessages.TasksInPastDeleted}", new[] { "other" }));
                                }
                            }
                        }

                        foreach (var task in tasks)
                        {
                            if (task.Start < earliestEditableTime)
                            {
                                var existing = existingDay?.Tasks.FirstOrDefault(t => t.Id == task.Id);

                                if (existing == null || task.Start != existing.Start || task.End != existing.End)
                                {
                                    validationResults.Add(new ValidationResult(ValidationMessages.TasksInPastEdited, ToControlName(tasks.IndexOf(task))));
                                }
                            }
                        }
                    }
                }
            }

            if (validationResults.Any())
            {
                return Invalid(ValidationMessages.TasksHaveErrors, validationResults.ToArray());
            }

            return Succeeded(input.ReturnValue);
        }

        private string[] ToControlName(int task)
        {
            return new[] { $"task-{task}" };
        }
    }
}

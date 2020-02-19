using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Directory;
using aiala.Backend.Models.Reports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Operations.Reports
{
    public class GetReportActivitiesOperation : OutputOperation<IEnumerable<ReportActivity>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ISettingsService _settingsService;

        public GetReportActivitiesOperation(AppDbContext dbContext, ISettingsService settingsService)
        {
            _dbContext = dbContext;
            _settingsService = settingsService;
        }

        protected override async Task<IOperationResult> Execute()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById((await _settingsService.Load<TenantSettings>(Context.TenantId.Value)).TimeZoneId);
            var minDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-30).Date, timeZone).Date;

            var activities = await _dbContext.Activities
                .Include(a => a.ActiveTask)
                    .ThenInclude(t => t.Day)
                .Include(a => a.ActiveTask)
                    .ThenInclude(t => t.Steps)
                .Where(a => a.Timestamp > minDate)
                .Where(a => a.Tenant.Id == Context.TenantId)
                .Where(a => ActivityTypeRanges.AllReports.Min <= (int)a.Type && ActivityTypeRanges.AllReports.Max >= (int)a.Type)
                .ToListAsync();

            var dayDates = activities.Where(a => a.ActiveTask == null)
                .Select(a => TimeZoneInfo.ConvertTimeFromUtc(a.Timestamp.DateTime, timeZone))
                .Select(d => d.Date)
                .Distinct();

            var days = await _dbContext.Days
                .Where(d => dayDates.Contains(d.Date.Date))
                .ToListAsync();

            var result = new List<ReportActivity>();
            foreach(var a in activities)
            {
                var localDate = TimeZoneInfo.ConvertTimeFromUtc(a.Timestamp.DateTime, timeZone);
                var ra = new ReportActivity
                {
                    Id = a.Id,
                    TimeOfDayMinutes = ToBlockAverage(localDate.TimeOfDay, 120),
                    Date = localDate.Date.ToShortDateString(),
                    ActivityType = a.Type,
                    ActivityData = a.ActivityData
                };

                var dayIndex = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday && a.Timestamp.DayOfWeek == DayOfWeek.Sunday
                    ? 7
                    : (int)a.Timestamp.DayOfWeek;
                ra.DayOfWeek = $"{dayIndex}: {Thread.CurrentThread.CurrentCulture.DateTimeFormat.DayNames[(int)a.Timestamp.DayOfWeek]}";
                if (a.ActiveTask != null)
                {
                    var taskDuration = a.ActiveTask.End - a.ActiveTask.Start;
                    var timeOffset = localDate.TimeOfDay - a.ActiveTask.Start;

                    var percentageTimeLate = Math.Max(0, ((timeOffset.TotalMinutes / taskDuration.TotalMinutes) * 100) - 100);

                    ra.DayName = a.ActiveTask.Day.Name;
                    ra.TaskName = a.ActiveTask.Name;
                    ra.TaskDuration = ToBlockAverage(taskDuration, 15);
                    ra.PercentageOfTaskLate = percentageTimeLate == 0 ? 0 : ToBlockAverage(percentageTimeLate, 10);
                    ra.WasCompletedLate = percentageTimeLate > 0;
                    ra.StepsCount = a.ActiveTask.Steps.Count;
                    if (a.ActiveTask.Steps.Count > 0)
                    {
                        ra.AverageStepTextLength = ToBlockAverage(a.ActiveTask.Steps.Select(s => s.Text.Length).Average(), 5);
                    }
                }
                else
                {
                    ra.DayName = days.FirstOrDefault(d => d.Date.Date == a.Timestamp.Date)?.Name;
                }

                if (ra.ActivityData != null)
                {
                    var activityDataObj = JObject.Parse(ra.ActivityData);

                    if (a.Type == ActivityType.EmergencyMood && activityDataObj.TryGetValue(EmergencyActivity.MoodDataKey, out var token) && token.Type == JTokenType.Integer)
                    {
                        var name = Enum.GetName(typeof(EmergencyMood), (int)token);
                        activityDataObj[EmergencyActivity.MoodDataKey] = $"{(int)token}: {name}";
                    }
                    else if (a.Type == ActivityType.TaskFeedback && activityDataObj.TryGetValue(ScheduledTaskActivity.FeedbackDataKey, out token) && token.Type == JTokenType.Integer)
                    {
                        var name = Enum.GetName(typeof(TaskFeedback), (int)token);
                        activityDataObj[ScheduledTaskActivity.FeedbackDataKey] = $"{(int)token}: {name}";
                    }
                    else if (a.Type == ActivityType.StepState && activityDataObj.TryGetValue(ScheduledStepActivity.StateDataKey, out token) && token.Type == JTokenType.Integer)
                    {
                        var name = Enum.GetName(typeof(StepState), (int)token);
                        activityDataObj[ScheduledStepActivity.StateDataKey] = $"{(int)token}: {name}";
                    }

                    ra.ActivityData = activityDataObj.ToString(Formatting.None);
                }

                result.Add(ra);
            }

            return Succeeded(result);
        }

        private int ToBlockAverage(TimeSpan time, int blockSize) => ToBlockAverage(time.TotalMinutes, blockSize);

        private int ToBlockAverage(double value, int blockSize) => Convert.ToInt32(Math.Floor(Math.Round(value / (blockSize)) * blockSize));
    }
}

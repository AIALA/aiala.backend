using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Schedule;
using xappido.Operations;

namespace aiala.Backend.Operations.Schedule
{
    public class FillDaysRangeOperation : InputOutputOperation<(DateTimeOffset from, DateTimeOffset to, IEnumerable<Day> days), IEnumerable<Day>>
    {
        protected override Task<IOperationResult> Execute((DateTimeOffset from, DateTimeOffset to, IEnumerable<Day> days) input)
        {
            var daysDifference = Math.Floor((input.to - input.from).TotalDays);
            var days = new List<Day>();
            for(var i = 0; i <= daysDifference; i++)
            {
                var date = input.from.AddDays(i);
                var day = input.days.FirstOrDefault(d => d.Date == date);
                if (day == null)
                {
                    day = new FilledDay
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        Tasks = new List<ScheduledTask>()
                    };
                }
                days.Add(day);
            }

            return Task.FromResult(Succeeded(days));
        }
    }
}

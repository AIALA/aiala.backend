using System;
using System.Linq;
using System.Threading.Tasks;
using aiala.Backend.ApiModels.Schedule;
using aiala.Backend.Models.Schedule;
using xappido.Operations;

namespace aiala.Backend.Mappings.Schedule
{
    public class UpsertedDayModelToUpsertedDayMapping : ModelMapping<UpsertedDayModel, UpsertedDay>
    {
        protected override Task<UpsertedDay> OnMap(UpsertedDayModel input, OperationContext context = null)
        {
            return Task.FromResult(new UpsertedDay
            {
                Id = input.Id ?? Guid.NewGuid(),
                Date = input.Date,
                Name = input.Name,
                Tasks = input.Tasks.Select(t => new UpsertedDayTask
                {
                    Id = t.Id ?? Guid.NewGuid(),
                    End = t.End,
                    Start = t.Start,
                    TaskId = t.TaskId
                }).ToList()
            });
        }
    }
}

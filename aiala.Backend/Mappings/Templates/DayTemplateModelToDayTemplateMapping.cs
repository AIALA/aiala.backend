using System;
using System.Linq;
using System.Threading.Tasks;
using aiala.Backend.ApiModels.Templates;
using aiala.Backend.Data.Templates;
using xappido.Operations;

namespace aiala.Backend.Mappings.Templates
{
    public class DayTemplateModelToDayTemplateMapping : ModelMapping<DayTemplateModel, DayTemplate>
    {
        protected override Task<DayTemplate> OnMap(DayTemplateModel input, OperationContext context = null)
        {
            return Task.FromResult(new DayTemplate
            {
                Id = input.Id ?? Guid.NewGuid(),
                Name = input.Name,
                DayName = input.DayName,
                Tasks = input.Tasks.Select(t => new ScheduledTaskTemplate
                {
                    Id = t.Id ?? Guid.NewGuid(),
                    TaskId = t.TaskId,
                    End = t.End,
                    Start = t.Start
                }).ToList()
            });
        }

        protected override Task<DayTemplateModel> OnMap(DayTemplate input, OperationContext context = null)
        {
            return Task.FromResult(new DayTemplateModel
            {
                Id = input.Id,
                Name = input.Name,
                DayName = input.DayName,
                Tasks = input.Tasks.Select(t => new ScheduledTaskTemplateModel
                {
                    Id = t.Id,
                    TaskId = t.TaskId,
                    End = t.End,
                    Start = t.Start
                }).ToList()
            });
        }
    }
}

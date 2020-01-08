using System;
using System.ComponentModel.DataAnnotations;
using aiala.Backend.Resources;

namespace aiala.Backend.ApiModels.Schedule
{
    public class UpsertedDayTaskModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public Guid TaskId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public TimeSpan Start { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public TimeSpan End { get; set; }
    }
}

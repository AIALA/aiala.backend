using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aiala.Backend.Resources;

namespace aiala.Backend.ApiModels.Schedule
{
    public class UpsertedDayModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public DateTimeOffset Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public string Name { get; set; }

        public List<UpsertedDayTaskModel> Tasks { get; set; }
    }
}

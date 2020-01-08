using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aiala.Backend.Resources;

namespace aiala.Backend.ApiModels.Templates
{
    public class DayTemplateModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public string DayName { get; set; }

        public List<ScheduledTaskTemplateModel> Tasks { get; set; }
    }
}

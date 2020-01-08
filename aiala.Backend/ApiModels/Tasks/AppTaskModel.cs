using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Resources;

namespace aiala.Backend.ApiModels.Tasks
{
    public class AppTaskModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        public TimeSpan Duration { get; set; }

        public Guid? PlaceId { get; set; }
        
        public bool UseTaskContacts { get; set; }

        public string FreeFormPlace { get; set; }

        public Guid? EmergencyContact1Id { get; set; }

        public Guid? EmergencyContact2Id { get; set; }

        public PictureModel Picture { get; set; }

        public IEnumerable<StepModel> Steps { get; set; } 
    }
}

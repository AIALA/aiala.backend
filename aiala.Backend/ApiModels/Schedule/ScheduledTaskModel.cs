using System;
using System.Collections.Generic;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.ApiModels.Places;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.ApiModels.Schedule
{
    public class ScheduledTaskModel
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public string Name { get; set; }

        public TimeSpan DefaultDuration { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public TaskFeedback Feedback { get; set; }

        public StepState State { get; set; }

        public TimeSpan ExpirationOffset { get; set; }

        public string FreeFormPlace { get; set; }

        public PictureModel Picture { get; set; }

        public PlaceAppModel Place { get; set; }

        public List<EmergencyContactModel> EmergencyContacts { get; set; }

        public List<ScheduledStepModel> Steps { get; set; }
    }
}
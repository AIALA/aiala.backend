using System;

namespace aiala.Backend.ApiModels.Tasks
{
    public class StepModel
    {
        public Guid? Id { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }
    }
}

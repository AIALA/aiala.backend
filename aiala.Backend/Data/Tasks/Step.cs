using System;

namespace aiala.Backend.Data.Tasks
{
    public class Step
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public AppTask Task { get; set; }
    }
}

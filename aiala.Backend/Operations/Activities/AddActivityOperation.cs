using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aiala.Backend.ApiModels.Activities;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using Newtonsoft.Json;
using xappido.Authorization;
using xappido.Operations;

namespace aiala.Backend.Operations.Activities
{
    public class AddActivityOperation : InputOutputOperation<AddActivityOperation.Request, Activity>
    {
        private readonly AppDbContext _dbContext;

        public AddActivityOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Request input)
        {
            var tenantId = Context.User.GetTenantId().Value;
            input.Activity.ActiveTask = input.Metadata.ActiveTaskId.HasValue
                ? await _dbContext.ScheduledTasks.FirstOrDefaultAsync(t => t.Day.Tenant.Id == tenantId && t.Id == input.Metadata.ActiveTaskId)
                : null;
            input.Activity.ActiveStep = input.Metadata.ActiveStepId.HasValue
                ? await _dbContext.ScheduledSteps.FirstOrDefaultAsync(s => s.Task.Day.Tenant.Id == tenantId && s.Id == input.Metadata.ActiveStepId)
                : null;

            if (input.Metadata.Longitude.HasValue && input.Metadata.Latitude.HasValue)
            {
                input.Activity.Latitude = input.Metadata.Latitude;
                input.Activity.Longitude = input.Metadata.Longitude;
            }
            else
            {
                input.Activity.Latitude = null;
                input.Activity.Longitude = null;
            }


            input.Activity.Timestamp = input.Metadata.Timestamp;
            input.Activity.Tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == Context.TenantId);
            input.Activity.TimeCreated = DateTimeOffset.UtcNow;
            input.Activity.Id = Guid.NewGuid();
            input.Activity.ActivityData = input.Data == null ? null : JsonConvert.SerializeObject(input.Data);

            _dbContext.Add(input.Activity);
            await _dbContext.SaveChangesAsync();

            return Succeeded(input.Activity);
        }

        public class Request
        {
            public Request(ActivityMetadataModel metadata, Activity activity, Dictionary<string, object> data = null)
            {
                Metadata = metadata;
                Activity = activity;
                Data = data;
            }

            public ActivityMetadataModel Metadata { get; }
            public Activity Activity { get; }
            public Dictionary<string, object> Data { get; }
        }
    }
}

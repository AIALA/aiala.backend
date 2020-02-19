using aiala.Backend.Models.Reports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Mappings.Reports
{
    public class ReportActivitiesToDictionaryMapping : ModelMapping<ReportActivity, object>
    {
        protected override Task<object> OnMap(ReportActivity input, OperationContext context = null)
        {
            var activityObj = JObject.FromObject(input, new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (input.ActivityData != null)
            {
                var dataObj = JObject.Parse(input.ActivityData);

                // To camel case
                foreach (var property in dataObj.Properties().ToList())
                {
                    dataObj[property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1)] = dataObj[property.Name];
                    dataObj.Remove(property.Name);
                }

                dataObj.Merge(activityObj);
                activityObj = dataObj;
            }

            return Task.FromResult((object)activityObj);
        }
    }
}

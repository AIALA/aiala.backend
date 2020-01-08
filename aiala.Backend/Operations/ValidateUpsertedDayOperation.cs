using System;
using System.Threading.Tasks;
using aiala.Backend.Resources;
using xappido.Operations;

namespace aiala.Backend.Operations
{
    public class ValidateIsUtcStartOfDayOperation : InputOutputOperation<(DateTimeOffset date, object passthrough), object>
    {
        protected override Task<IOperationResult> Execute((DateTimeOffset date, object passthrough) input)
        {
            if (input.date.Offset != TimeSpan.Zero || input.date.TimeOfDay != TimeSpan.Zero)
            {
                return Task.FromResult(Invalid(ValidationMessages.DateMustBeUTCStartOfDay));
            }

            return Task.FromResult(Succeeded(input.passthrough));
        }
    }
}

using System;
using System.Threading.Tasks;
using aiala.Backend.Data.Activities;
using aiala.Backend.Services;
using xappido.Operations;

namespace aiala.Backend.Operations.Notifications
{
    public class EnqueueEmergencyNotificationOperation : InputOperation<EmergencyActivity>
    {
        private readonly IEmergencyNotificationsQueueService _queueService;

        public EnqueueEmergencyNotificationOperation(IEmergencyNotificationsQueueService queueService)
        {
            _queueService = queueService;
        }

        protected override Task<IOperationResult> Execute(EmergencyActivity input)
        {
            _queueService.Enqueue(input.EmergencyId, input.Type, DateTimeOffset.UtcNow);
            return Task.FromResult(Succeeded());
        }
    }
}

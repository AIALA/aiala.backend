using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using aiala.Backend.Data.Activities;
using aiala.Backend.Options;

namespace aiala.Backend.Services
{
    public interface IEmergencyNotificationsQueueService
    {
        void Enqueue(Guid id, ActivityType activityType, DateTimeOffset now);

        bool TryDequeue(out QueuedEmergency result, DateTimeOffset now);
    }

    public class EmergencyNotificationsQueueService : IEmergencyNotificationsQueueService
    {
        private readonly List<QueuedEmergency> _queuedEmergencies = new List<QueuedEmergency>();
        private readonly NotificationWorkerOptions _options;

        public EmergencyNotificationsQueueService(IOptions<NotificationWorkerOptions> options)
        {
            _options = options.Value;
        }

        public bool TryDequeue(out QueuedEmergency result, DateTimeOffset now)
        {
            result = _queuedEmergencies.Where(e => e.EnqueueTime.AddSeconds(_options.NotificationBufferDuration) <= now).FirstOrDefault();

            if (result == null)
            {
                return false;
            }
            else
            {
                _queuedEmergencies.Remove(result);
                return true;
            }
        }

        public void Enqueue(Guid id, ActivityType activityType, DateTimeOffset now)
        {
            var existing = _queuedEmergencies.FirstOrDefault(e => e.Id == id);

            if (existing != null)
            {
                // All other types take priority over mood.
                if (activityType == ActivityType.EmergencyMood)
                {
                    return;
                }

                existing.Type = activityType;
                return;
            }

            _queuedEmergencies.Add(new QueuedEmergency
            {
                Id = id,
                Type = activityType,
                EnqueueTime = now
            });
        }
    }

    public class QueuedEmergency
    {
        public Guid Id { get; set; }

        public ActivityType Type { get; set; }

        public DateTimeOffset EnqueueTime { get; set; }
    }
}

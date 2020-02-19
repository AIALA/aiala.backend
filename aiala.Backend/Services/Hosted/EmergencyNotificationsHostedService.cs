using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using aiala.Backend.Data;
using aiala.Backend.Data.Activities;
using aiala.Backend.Models.Directory;
using aiala.Backend.Operations.Notifications;
using aiala.Backend.Options;
using xappido.Directory.Settings.Services;
using xappido.Operations;

namespace aiala.Backend.Services.Hosted
{
    public class EmergencyNotificationsHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly NotificationWorkerOptions _options;
        private readonly ILogger<EmergencyNotificationsHostedService> _logger;
        private readonly IEmergencyNotificationsQueueService _queueService;
        private readonly IServiceProvider _provider;

        public EmergencyNotificationsHostedService(
            IOptions<NotificationWorkerOptions> options,
            ILogger<EmergencyNotificationsHostedService> logger,
            IEmergencyNotificationsQueueService queueService,
            IServiceProvider provider)
        {
            _options = options.Value;
            _logger = logger;
            _queueService = queueService;
            _provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Process, null, TimeSpan.Zero, TimeSpan.FromSeconds(_options.ProcessInterval));

            return Task.CompletedTask;
        }

        private void Process(object state)
        {
            _logger.LogTrace("Processing emergency notifications");

            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var settingsService = scope.ServiceProvider.GetRequiredService<ISettingsService>();

                while (_queueService.TryDequeue(out var queuedEmergency, DateTimeOffset.UtcNow))
                {
                    _logger.LogDebug("Processing emergency notification with id {id} and type {type}", queuedEmergency.Id, queuedEmergency.Type);

                    var activity = dbContext.EmergencyActivities
                        .Include(a => a.Tenant)
                        .OrderByDescending(a => a.Timestamp)
                        .FirstOrDefault(a => a.EmergencyId == queuedEmergency.Id);

                    if (activity == null)
                    {
                        _logger.LogWarning("Could not send notification for emergency with id {id} because no activity was found", queuedEmergency.Id);
                        continue;
                    }

                    var tenantSettings = settingsService.Load<TenantSettings>(activity.Tenant.Id).Result;
                    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = tenantSettings.TenantCulture.ToCultureInfo();

                    var result = scope.ServiceProvider.GetRequiredService<IOperationExecutor>()
                        .Add<UpdateEmergencyNotificationOperation, EmergencyActivity>(() => activity)
                        .Execute().Result;

                    if (result.ResultType != OperationResultType.Succeeded)
                    {
                        _logger.LogCritical("Could not send notification for emergency with id {id}, operation error: {message}", queuedEmergency.Id, result.Message);
                    }
                    else
                    {
                        _logger.LogDebug("Sent message for emergency with id {id}", queuedEmergency.Id);
                    }
                }
            }

            _logger.LogTrace("No emergency notifications to process anymore");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

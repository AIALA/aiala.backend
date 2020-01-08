using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data.Activities;
using aiala.Backend.Options;
using aiala.Backend.Services;

namespace aiala.Backend.UnitTests.Services
{
    [TestCategory("Services.EmergencyNotificationsQueueService")]
    [TestClass]
    public class EmergencyNotificationsQueueServiceTests
    {
        private IOptions<NotificationWorkerOptions> _options;
        private DateTimeOffset _now;
        private DateTimeOffset _beforeSendtime;
        private DateTimeOffset _afterSendTime;

        [TestInitialize]
        public void Initialize()
        {
            _options = new OptionsWrapper<NotificationWorkerOptions>(new NotificationWorkerOptions
            {
                NotificationBufferDuration = 15
            });

            _now = 9.April(2019).At(7.Hours(6.Minutes()));
            _beforeSendtime = _now.Add((_options.Value.NotificationBufferDuration - 1).Seconds());
            _afterSendTime = _now.Add(_options.Value.NotificationBufferDuration.Seconds());
        }

        [TestMethod]
        public void AddNew_ShouldHaveNew()
        {
            // Arrange
            var emergencyId = Guid.NewGuid();
            var now = 9.April(2019).At(7.Hours(6.Minutes()));
            var sut = new EmergencyNotificationsQueueService(_options);

            // Act
            sut.Enqueue(emergencyId, ActivityType.EmergencyStart, now);

            // Assert
            var canDequeue = sut.TryDequeue(out var result, _afterSendTime);
            result.Id.Should().Be(emergencyId);
            result.Type.Should().Be(ActivityType.EmergencyStart);

            sut.TryDequeue(out _, _afterSendTime).Should().BeFalse();
        }

        [TestMethod]
        public void AddStartAndMood_ShouldHaveStart()
        {
            // Arrange
            var emergencyId = Guid.NewGuid();
            var now = 9.April(2019).At(7.Hours(6.Minutes()));
            var sut = new EmergencyNotificationsQueueService(_options);

            // Act
            sut.Enqueue(emergencyId, ActivityType.EmergencyStart, now);
            sut.Enqueue(emergencyId, ActivityType.EmergencyMood, now);

            // Assert
            var canDequeue = sut.TryDequeue(out var result, _afterSendTime);
            canDequeue.Should().BeTrue();
            result.Id.Should().Be(emergencyId);
            result.Type.Should().Be(ActivityType.EmergencyStart);

            sut.TryDequeue(out _, _afterSendTime).Should().BeFalse();
        }

        [TestMethod]
        public void AddMoodAndEnd_ShouldHaveEnd()
        {
            // Arrange
            var emergencyId = Guid.NewGuid();
            var now = 9.April(2019).At(7.Hours(6.Minutes()));
            var sut = new EmergencyNotificationsQueueService(_options);

            // Act
            sut.Enqueue(emergencyId, ActivityType.EmergencyMood, now);
            sut.Enqueue(emergencyId, ActivityType.EmergencyEnd, now);

            // Assert
            var canDequeue = sut.TryDequeue(out var result, _afterSendTime);
            canDequeue.Should().BeTrue();
            result.Id.Should().Be(emergencyId);
            result.Type.Should().Be(ActivityType.EmergencyEnd);

            sut.TryDequeue(out _, _afterSendTime).Should().BeFalse();
        }

        [TestMethod]
        public void AddTwoEmergencies_ShouldHaveBoth()
        {
            // Arrange
            var emergencyId1 = Guid.NewGuid();
            var emergencyId2 = Guid.NewGuid();
            var now = 9.April(2019).At(7.Hours(6.Minutes()));
            var sut = new EmergencyNotificationsQueueService(_options);

            // Act
            sut.Enqueue(emergencyId1, ActivityType.EmergencyMood, now);
            sut.Enqueue(emergencyId2, ActivityType.EmergencyStart, now);

            // Assert
            var canDequeue = sut.TryDequeue(out var result, _afterSendTime);
            canDequeue.Should().BeTrue();
            result.Id.Should().Be(emergencyId1);
            result.Type.Should().Be(ActivityType.EmergencyMood);

            canDequeue = sut.TryDequeue(out result, _afterSendTime);
            canDequeue.Should().BeTrue();
            result.Id.Should().Be(emergencyId2);
            result.Type.Should().Be(ActivityType.EmergencyStart);

            sut.TryDequeue(out _, _afterSendTime).Should().BeFalse();
        }

        [TestMethod]
        public void AddNew_After2Seconds_ShouldHaveNone()
        {
            // Arrange
            var emergencyId = Guid.NewGuid();
            var now = 9.April(2019).At(7.Hours(6.Minutes()));
            var sut = new EmergencyNotificationsQueueService(_options);

            // Act
            sut.Enqueue(emergencyId, ActivityType.EmergencyStart, now);

            // Assert
            sut.TryDequeue(out _, _beforeSendtime).Should().BeFalse();
            sut.TryDequeue(out _, _afterSendTime).Should().BeTrue();
        }
    }
}

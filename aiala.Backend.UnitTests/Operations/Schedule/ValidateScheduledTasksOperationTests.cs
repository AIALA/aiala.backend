using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Models.Schedule;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    [TestCategory("Operations.Schedule.ValidateScheduledTasks")]
    [TestClass]
    public class ValidateScheduledTasksOperationTests
    {
        [TestMethod]
        public async Task Execute_TasksWithOneMinuteOverlap_ShouldBeSuccessful()
        {
            // Arrange
            var returnValue = "ret";
            var model = new ScheduledTasksValidationModel(
                returnValue,
                new List<UpsertedDayTask>
                {
                    new UpsertedDayTask
                    {
                        Start = 5.Hours(),
                        End = 8.Hours()
                    },
                    new UpsertedDayTask
                    {
                        Start = 8.Hours(),
                        End = 9.Hours()
                    }
                }.AsEnumerable(),
                null);

            var sut = new ValidateScheduledTasksOperationBuilder()
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeSucceeded();
            result.Result.Should().Be(returnValue);
        }

        [TestMethod]
        public async Task Execute_TasksWithTwoMinutesOverlap_ShouldBeInvalid()
        {
            // Arrange
            var returnValue = "ret";
            var model = new ScheduledTasksValidationModel(
                returnValue,
                new List<UpsertedDayTask>
                {
                    new UpsertedDayTask
                    {
                        Start = 5.Hours(),
                        End = 8.Hours(1.Minutes())
                    },
                    new UpsertedDayTask
                    {
                        Start = 8.Hours(),
                        End = 9.Hours()
                    }
                }.AsEnumerable(),
                null);

            var sut = new ValidateScheduledTasksOperationBuilder()
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeInvalid();
        }
    }
}

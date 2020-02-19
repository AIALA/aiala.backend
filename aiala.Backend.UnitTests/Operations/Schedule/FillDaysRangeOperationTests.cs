using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data.Schedule;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    [TestClass]
    [TestCategory("Operations.Schedule.FillDaysRange")]
    public class FillDaysRangeOperationTests
    {
        [TestMethod]
        public async Task Execute_ShouldFillRangeWithDefaultDays()
        {
            // Arrange
            var from = (DateTimeOffset)19.January(2019);
            var to = (DateTimeOffset)23.January(2019);

            var existingDays = new List<Day>
            {
                new Day
                {
                    Id = Guid.NewGuid(),
                    Date = 20.January(2019)
                },
                new Day
                {
                    Id = Guid.NewGuid(),
                    Date = 22.January(2019)
                }
            };

            var expectedDays = new List<Day>
            {
                new Day
                {
                    Date = from,
                    Tasks = new List<ScheduledTask>()
                },
                existingDays[0],
                new Day
                {
                    Date = 21.January(2019),
                    Tasks = new List<ScheduledTask>()
                },
                existingDays[1],
                new Day
                {
                    Date = to,
                    Tasks = new List<ScheduledTask>()
                }
            };

            var builder = new FillDaysRangeOperationBuilder();
            var sut = builder.Build();

            // Act
            var result = await sut.Execute((from, to, existingDays as IEnumerable<Day>));

            // Assert
            result.Should().BeSucceeded();
            result.Result.Should().BeEquivalentTo(expectedDays, options => options.ExcludingIds());
        }
    }
}

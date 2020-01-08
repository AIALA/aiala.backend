using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Data.Tasks;
using aiala.Backend.Models.Schedule;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    [TestClass]
    [TestCategory("Operations.Schedule.UpsertDay")]
    public class UpsertDayOperationTests
    {
        [TestMethod]
        public async Task UpsertNewDay_ShouldAddDay()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var addedTaskId = Guid.NewGuid();
            var addedTaskTemplateId = Guid.NewGuid();
            var tenant = new Tenant
            {
                Id = groupId,
                Name = "group"
            };

            var existingDay = new Day
            {
                Id = Guid.NewGuid(),
                Date = 9.January(2019),
                Name = "existing",
                Tenant = tenant
            };
            var upsertedDay = new UpsertedDay
            {
                Id = Guid.NewGuid(),
                Date = 11.January(2019),
                Name = "new",
                Tasks = new List<UpsertedDayTask>
                {
                    new UpsertedDayTask
                    {
                        Id = addedTaskTemplateId,
                        TaskId = addedTaskId,
                        Start = 2.Hours(30.Minutes()),
                        End = 5.Hours()
                    }
                }
            };
            var addedTask = new AppTask
            {
                Id = addedTaskId,
                Name = "task",
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.NewGuid(),
                        Order = 0,
                        Text = "step"
                    }
                }
            };
            var expectedDays = new List<Day>
            {
                existingDay,
                new Day
                {
                    Id = upsertedDay.Id,
                    Date = upsertedDay.Date,
                    Name = upsertedDay.Name,
                    Tenant = tenant,
                    Tasks = new List<ScheduledTask>
                    {
                        new ScheduledTask
                        {
                            Id = addedTaskId,
                            Name = addedTask.Name,
                            Start = upsertedDay.Tasks.Single().Start,
                            End = upsertedDay.Tasks.Single().End,
                            Task = addedTask,
                            Steps = new List<ScheduledStep>
                            {
                                new ScheduledStep
                                {
                                    Id = Guid.NewGuid(),
                                    Order = 0,
                                    Text = "step"
                                }
                            }
                        }
                    }
                }
            };

            var builder = new UpsertDayOperationBuilder()
                .WithEntity(existingDay)
                .WithEntity(addedTask)
                .WithEntity(new Tenant { Id = Guid.NewGuid(), Name = "wrong" });
            var sut = builder.Build();

            // Act
            var result = await sut.Execute((groupId, upsertedDay));

            // Assert
            result.Should().BeSucceeded();
            builder.DbContext.Days.Should().BeEquivalentTo(expectedDays, ApplyExcludesForDays);
            builder.DbContext.ScheduledTasks.Should().BeEquivalentTo(new List<ScheduledTask> { expectedDays[1].Tasks.Single() }, ApplyExcludesForDays);
            builder.DbContext.ScheduledSteps.Should().BeEquivalentTo(new List<ScheduledStep> { expectedDays[1].Tasks.Single().Steps.Single() }, ApplyExcludesForDays);
        }

        [TestMethod]
        public async Task UpsertExistingDay_ShouldUpdateDay()
        {
            // Arrange
            var updatedId = Guid.NewGuid();
            var updatedScheduledTaskId = Guid.NewGuid();
            var updatedTaskId = Guid.NewGuid();
            var tenant = new Tenant
            {
                Id = Guid.NewGuid()
            };
            var newTask = new AppTask
            {
                Id = Guid.NewGuid(),
                Name = "newTask",
                Steps = new List<Step>()
            };
            var existingDays = new List<Day>
            {
                new Day
                {
                    Id = Guid.NewGuid(),
                    Date = 9.January(2019),
                    Name = "existing"
                },
                new Day
                {
                    Id = updatedId,
                    Date = 10.January(2019),
                    Name = "old",
                    Tasks = new List<ScheduledTask>
                    {
                        new ScheduledTask
                        {
                            Id = Guid.NewGuid(),
                            Start = 4.Hours(30.Minutes()),
                            End = 6.Hours()
                        },
                        new ScheduledTask
                        {
                            Id = updatedScheduledTaskId,
                            Start = 2.Hours(30.Minutes()),
                            End = 3.Hours(),
                            Name = "name",
                            Task = new AppTask
                            {
                                Id = updatedTaskId,
                                Name = "task"
                            },
                            Steps = new List<ScheduledStep>
                            {
                                new ScheduledStep
                                {
                                    Id = Guid.NewGuid(),
                                    Text = "Step"
                                }
                            }
                        }
                    }
                }
            };
            var upsertedDay = new UpsertedDay
            {
                Id = updatedId,
                Name = "new",
                Tasks = new List<UpsertedDayTask>
                {
                    new UpsertedDayTask
                    {
                        Id = updatedScheduledTaskId,
                        TaskId = updatedTaskId,
                        Start = 3.Hours(30.Minutes()),
                        End = 5.Hours()
                    },
                    new UpsertedDayTask
                    {
                        Id = Guid.NewGuid(),
                        TaskId = newTask.Id,
                        Start = 5.Hours(),
                        End = 7.Hours()
                    }
                }
            };
            var upsertedDbDay = new Day
            {
                Id = updatedId,
                Date = 10.January(2019),
                Name = upsertedDay.Name,
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Id = updatedScheduledTaskId,
                        Start = 3.Hours(30.Minutes()),
                        End = 5.Hours(),
                        Name = "name",
                        Task = existingDays[1].Tasks.Last().Task,
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Text = "Step"
                            }
                        }
                    },
                    new ScheduledTask
                    {
                        Id = Guid.Empty,
                        Task = newTask,
                        Name = newTask.Name,
                        Start = 5.Hours(),
                        End = 7.Hours(),
                        Steps = new List<ScheduledStep>()
                    }
                }
            };
            var expectedDays = new List<Day>
            {
                existingDays[0],
                new Day
                {
                    Id = updatedId,
                    Date = existingDays[1].Date,
                    Name = upsertedDbDay.Name,
                    Tasks = upsertedDbDay.Tasks
                }
            };

            var builder = new UpsertDayOperationBuilder()
                .WithEntity(existingDays.ToArray())
                .WithEntity(newTask);

            var sut = builder.Build();

            // Act
            var result = await sut.Execute((tenant.Id, upsertedDay));

            // Assert
            result.Should().BeSucceeded();
            builder.DbContext.Days.Should().BeEquivalentTo(expectedDays, ApplyExcludesForDays);
            builder.DbContext.ScheduledTasks.Should().BeEquivalentTo(upsertedDbDay.Tasks, ApplyExcludesForDays);
        }

        private EquivalencyAssertionOptions<IEnumerable> ApplyExcludesForDays(EquivalencyAssertionOptions<IEnumerable> options)
        {
            return options
                .ExcludingIds()
                .Excluding(nameof(ScheduledStep.Task))
                .Excluding(nameof(ScheduledTask.Day));
        }
    }
}

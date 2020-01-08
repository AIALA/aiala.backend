using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data;
using aiala.Backend.Data.Tasks;

namespace aiala.Backend.UnitTests.Operations.Tasks
{
    [TestCategory("Operations.Tasks.UpsertTask")]
    [TestClass]
    public class UpsertTaskOperationTests
    {
        [TestMethod]
        public async Task Insert_ShouldSetDefaultsAndInsert()
        {
            // Arrange
            var expectedGroup = new Tenant
            {
                Id = Guid.Parse("26582903-1111-486D-8258-1F03C72EBDAA")
            };
            var expectedMember = new Account
            {
                Id = Guid.Parse("26582903-3333-486D-8258-1F03C72EBDAA"),
                Tenant = expectedGroup
            };

            var addedTask = new AppTask
            {
                Id = Guid.Empty,
                Name = "Name",
                Duration = TimeSpan.FromMinutes(1.5),
                Place = null,
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.Empty,
                        Text = "Step"
                    }
                }
            };
            var expectedTask = new AppTask
            {
                Id = Guid.Empty,
                Name = "Name",
                Duration = TimeSpan.FromMinutes(1.5),
                Place = null,
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.Empty,
                        Text = "Step"
                    }
                },
                Author = expectedMember,
                Tenant = expectedGroup,
                LastModified = DateTimeOffset.Now
            };

            var sutBuilder = new UpsertTaskOperationBuilder()
                .WithEntity(expectedMember, new Account { Id = Guid.NewGuid() })
                .WithEntity(new User { Id = Guid.NewGuid() });
            var sut = sutBuilder.Build();

            // Act
            var result = await sut.Execute((expectedMember.Id, addedTask));

            // Assert
            result.Should().BeSucceeded();
            result.Result.Should().BeEquivalentTo(expectedTask, ApplyExcludesForTask);
            sutBuilder.DbContext.Tasks.Should().BeEquivalentTo(new List<AppTask>
                {
                    expectedTask
                },
                ApplyExcludesForTask
            );
        }

        [TestMethod]
        public async Task Update_ShouldUpdateSuccessfully()
        {
            // Arrange
            var taskId = Guid.Parse("26582903-0000-486D-8258-1F03C72EBDBB");
            var expectedTenant = new Tenant
            {
                Id = Guid.Parse("26582903-1111-486D-8258-1F03C72EBDBB")
            };
            var expectedMember = new Account
            {
                Id = Guid.Parse("26582903-3333-486D-8258-1F03C72EBDBB"),
                Tenant = expectedTenant
            };

            var updatedTask = new AppTask
            {
                Id = taskId,
                Name = "Name",
                Duration = TimeSpan.FromMinutes(1.5),
                Place = null,
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.Empty,
                        Text = "Step"
                    }
                }
            };
            var existingTask = new AppTask
            {
                Id = taskId,
                Name = "Old Name",
                Duration = TimeSpan.FromMinutes(3),
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.NewGuid(),
                        Text = "Old Step"
                    }
                },
                Author = null,
                Tenant = null,
                LastModified = DateTimeOffset.FromUnixTimeMilliseconds(0)
            };
            var expectedTask = new AppTask
            {
                Id = taskId,
                Name = "Name",
                Duration = TimeSpan.FromMinutes(1.5),
                Place = null,
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = Guid.Empty,
                        Text = "Step"
                    }
                },
                Author = expectedMember,
                Tenant = expectedTenant,
                LastModified = DateTimeOffset.Now
            };

            var sutBuilder = new UpsertTaskOperationBuilder()
                .WithEntity(expectedMember, new Account { Id = Guid.NewGuid() })
                .WithEntity(new User { Id = Guid.NewGuid() })
                .WithEntity(existingTask);
            var sut = sutBuilder.Build();

            // Act
            var result = await sut.Execute((expectedMember.Id, updatedTask));

            // Assert
            result.Should().BeSucceeded();
            result.Result.Should().BeEquivalentTo(expectedTask, ApplyExcludesForTask);
            sutBuilder.DbContext.Tasks.Should().BeEquivalentTo(new List<AppTask>
                {
                    expectedTask
                },
                ApplyExcludesForTask
            );
        }

        private EquivalencyAssertionOptions<AppTask> ApplyExcludesForTask(EquivalencyAssertionOptions<AppTask> options)
        {
            return options
                .ExcludingIds()
                .Excluding(t => t.LastModified)
                .Excluding(nameof(Step.Task));
        }
    }
}

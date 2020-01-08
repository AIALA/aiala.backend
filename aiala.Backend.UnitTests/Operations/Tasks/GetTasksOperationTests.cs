using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data;
using aiala.Backend.Data.Tasks;

namespace aiala.Backend.UnitTests.Operations.Tasks
{
    [TestCategory("Operations.Tasks.GetTasks")]
    [TestClass]
    public class GetTasksOperationTests
    {
        [TestMethod]
        public async Task Get_ShouldReturnAllTasks()
        {
            // Arrange
            var tenantId = Guid.Parse("F5B602EC-86E0-4EBB-80F6-415708404583");
            var tasks = new List<AppTask>
            {
                new AppTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Name1",
                    Tenant = new Tenant
                    {
                        Id = Guid.NewGuid()
                    }
                },
                new AppTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Name2",
                    Tenant = new Tenant
                    {
                        Id = tenantId
                    }
                },
                new AppTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Name3",
                    Tenant = new Tenant
                    {
                        Id = tenantId
                    }
                }
            };

            var sut = new GetTasksOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(tasks.ToArray())
                .Build();

            // Act
            var result = await sut.Execute();

            // Assert
            result.Should().BeSucceeded();
            result.Result.Should().BeEquivalentTo(new List<AppTask>
            {
                tasks[1],
                tasks[2]
            });
        }
    }
}

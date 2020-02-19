using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aiala.Backend.Data;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Models.Activities;

namespace aiala.Backend.UnitTests.Operations.Activities
{
    [TestClass]
    [TestCategory("Operations.Activities.ValidateStepStateChange")]
    public class ValidateStepStateChangeOperationTests
    {
        [TestMethod]
        public async Task Execute_Done_EarlierStepsDone_Succeeds()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 7.Hours(),
                        End = 9.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 1,
                                Text = "Step to set done",
                                State = StepState.Undone
                            },
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 0,
                                Text = "Done Step",
                                State = StepState.Done
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Done
            };

            var builder = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day);
            var sut = builder.Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeSucceeded();
        }

        [TestMethod]
        public async Task Execute_Done_EarlierStepNotDone_Invalid()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 7.Hours(),
                        End = 9.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 1,
                                Text = "Step to set done",
                                State = StepState.Undone
                            },
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 0,
                                Text = "Undone Step",
                                State = StepState.Undone
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Done
            };

            var sut = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day)
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeInvalid();
        }

        [TestMethod]
        public async Task Execute_Done_EarlierTaskStepNotDone_Invalid()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 9.Hours(),
                        End = 10.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 1,
                                Text = "Step to set done",
                                State = StepState.Undone
                            }
                        }
                    },
                    new ScheduledTask
                    {
                        Start = 7.Hours(),
                        End = 9.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 0,
                                Text = "Undone Step",
                                State = StepState.Undone
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Done
            };

            var sut = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day)
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeInvalid();
        }

        [TestMethod]
        public async Task Execute_Undone_LaterStepsUndone_Succeeds()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 7.Hours(),
                        End = 9.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 0,
                                Text = "Step to set undone",
                                State = StepState.Done
                            },
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 1,
                                Text = "Undone Step",
                                State = StepState.Undone
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Undone
            };

            var builder = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day);
            var sut = builder.Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeSucceeded();
        }

        [TestMethod]
        public async Task Execute_Undone_LaterStepsDone_Invalid()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 7.Hours(),
                        End = 9.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 0,
                                Text = "Step to set undone",
                                State = StepState.Done
                            },
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 1,
                                Text = "Done Step",
                                State = StepState.Done
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Undone
            };

            var builder = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day);
            var sut = builder.Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeInvalid();
        }

        [TestMethod]
        public async Task Execute_Undone_LaterTaskStepNotDone_Invalid()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = tenantId
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 9.Hours(),
                        End = 10.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 1,
                                Text = "Step to set undone",
                                State = StepState.Done
                            }
                        }
                    },
                    new ScheduledTask
                    {
                        Start = 10.Hours(),
                        End = 11.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = Guid.NewGuid(),
                                Order = 0,
                                Text = "Done Step",
                                State = StepState.Done
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Undone
            };

            var sut = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day)
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeInvalid();
        }

        [TestMethod]
        public async Task Execute_WrongTenant_NotAllowed()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var stepId = Guid.NewGuid();

            var day = new Day
            {
                Tenant = new Tenant
                {
                    Id = Guid.NewGuid()
                },
                Tasks = new List<ScheduledTask>
                {
                    new ScheduledTask
                    {
                        Start = 9.Hours(),
                        End = 10.Hours(),
                        Steps = new List<ScheduledStep>
                        {
                            new ScheduledStep
                            {
                                Id = stepId,
                                Order = 0,
                                Text = "Step to set done",
                                State = StepState.Undone
                            }
                        }
                    }
                }
            };

            var model = new UpdateStepStateOperationsModel
            {
                StepId = stepId,
                NewStepState = StepState.Done
            };

            var sut = new ValidateStepStateChangeOperationBuilder()
                .WithContext(tenantId)
                .WithEntity(day)
                .Build();

            // Act
            var result = await sut.Execute(model);

            // Assert
            result.Should().BeNotFound();
        }
    }
}

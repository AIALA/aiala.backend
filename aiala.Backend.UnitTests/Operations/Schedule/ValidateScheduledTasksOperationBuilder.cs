using aiala.Backend.Operations.Schedule;
using xappido.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    class ValidateScheduledTasksOperationBuilder : OperationSutBuilder<ValidateScheduledTasksOperationBuilder, ValidateScheduledTasksOperation>
    {
        protected override ValidateScheduledTasksOperation OnBuildOperation() => new ValidateScheduledTasksOperation(null);
    }
}

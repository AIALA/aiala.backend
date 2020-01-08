using aiala.Backend.Data;
using aiala.Backend.Operations.Activities;
using aiala.Backend.UnitTests.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Activities
{
    public class ValidateStepStateChangeOperationBuilder
        : ContextOperationBuilder<ValidateStepStateChangeOperationBuilder, ValidateStepStateChangeOperation, AppDbContext>
    {
        protected override ValidateStepStateChangeOperation OnBuildOperation(AppDbContext dbContext)
        {
            return new ValidateStepStateChangeOperation(dbContext);
        }
    }
}

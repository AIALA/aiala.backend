using aiala.Backend.Operations.Schedule;
using xappido.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    public class FillDaysRangeOperationBuilder : OperationSutBuilder<FillDaysRangeOperationBuilder, FillDaysRangeOperation>
    {
        protected override FillDaysRangeOperation OnBuildOperation()
        {
            return new FillDaysRangeOperation();
        }
    }
}

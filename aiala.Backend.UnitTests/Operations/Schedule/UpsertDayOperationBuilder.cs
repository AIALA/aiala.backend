using aiala.Backend.Data;
using aiala.Backend.Operations.Schedule;
using xappido.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Schedule
{
    public class UpsertDayOperationBuilder : OperationSutBuilder<UpsertDayOperationBuilder, UpsertDayOperation, AppDbContext>
    {
        protected override UpsertDayOperation OnBuildOperation(AppDbContext dbContext)
        {
            return new UpsertDayOperation(dbContext);
        }
    }
}

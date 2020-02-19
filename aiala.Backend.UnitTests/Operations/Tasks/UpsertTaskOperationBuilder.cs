using aiala.Backend.Data;
using aiala.Backend.Operations.Tasks;
using xappido.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Tasks
{
    public class UpsertTaskOperationBuilder : OperationSutBuilder<UpsertTaskOperationBuilder, UpsertTaskOperation, AppDbContext>
    {
        protected override UpsertTaskOperation OnBuildOperation(AppDbContext dbContext)
        {
            return new UpsertTaskOperation(dbContext);
        }
    }
}

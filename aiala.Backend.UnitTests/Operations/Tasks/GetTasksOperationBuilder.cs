using aiala.Backend.Data;
using aiala.Backend.Operations.Tasks;
using aiala.Backend.UnitTests.TestHelpers;

namespace aiala.Backend.UnitTests.Operations.Tasks
{
    public class GetTasksOperationBuilder : ContextOperationBuilder<GetTasksOperationBuilder, GetTasksOperation, AppDbContext>
    {
        protected override GetTasksOperation OnBuildOperation(AppDbContext dbContext) => new GetTasksOperation(dbContext);
    }
}

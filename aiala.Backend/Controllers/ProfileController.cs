using aiala.Backend.Data;
using xappido.Directory.Controllers;
using xappido.Operations;

namespace aiala.Backend.Controllers
{
    public class ProfileController : ProfileController<AppDbContext, Tenant, Account, User>
    {
        public ProfileController(IOperationExecutor executor) : base(executor)
        {
        }
    }
}

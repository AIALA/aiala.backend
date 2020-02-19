using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using xappido.Directory;
using xappido.Operations;
using xappido.TestHelpers;

namespace aiala.Backend.UnitTests.TestHelpers
{
    public abstract class ContextOperationBuilder<TBuilder, TOperation, TDbContext> : OperationSutBuilder<TBuilder, TOperation, TDbContext>
        where TBuilder : class
        where TOperation : class, IOperation
        where TDbContext : DbContext
    {
        public TBuilder WithContext(Guid tenantId)
        {
            var context = new OperationContext(
                new ClaimsPrincipalBuilder()
                    .WithClaims(new Claim(DirectoryClaimTypes.Tenant, tenantId.ToString()))
                    .Build());

            return WithContext(context);
        }
    }
}

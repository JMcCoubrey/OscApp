using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SaasKit.Multitenancy;
using Osc.Db;
using Microsoft.AspNetCore.Identity;

namespace OscApp.Web
{
    public class CachingTenantResolver : MemoryCacheTenantResolver<Tenancy>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public CachingTenantResolver(
            ApplicationDbContext context, IMemoryCache cache, ILoggerFactory loggerFactory, UserManager<ApplicationUser> manager)
             : base(cache, loggerFactory)
        {
            _context = context;
            _manager = manager;
        }

        // Resolver runs on cache misses
        protected override async Task<TenantContext<Tenancy>> ResolveAsync(HttpContext context)
        {
            var userId = _manager.GetUserId(context.User);

            var tenancyUser = await _context.TenancyUsers
                .FirstOrDefaultAsync(u => u.UserId == userId);

            var tenant = await _context.Tenancies
                .FirstOrDefaultAsync(t => t.TenancyId == tenancyUser.TenancyId);

            if (tenant == null) return null;

            return new TenantContext<Tenancy>(tenant);
        }

        protected override MemoryCacheEntryOptions CreateCacheEntryOptions()
            => new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

        protected override string GetContextIdentifier(HttpContext context)
            => _manager.GetUserId(context.User);
            
        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<Tenancy> context)
            => new string[] { context.Tenant.TenancyId.ToString() };
    }
}

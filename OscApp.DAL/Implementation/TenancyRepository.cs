using System.Collections.Generic;
using Osc.Db;

namespace OscApp.DAL.Implementation
{
    public class TenancyRepository : ITenancyRepository
    {
        private IApplicationDbContext _dbContext;

        public TenancyRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateTenancy(string name, string userId)
        {
            var newTenancy = _dbContext.Tenancies.Add(new Tenancy()
            {
                DisplayName = name,
                TenancyUsers = new List<TenancyUser>()
                {
                    new TenancyUser() { UserId = userId }
                }
            });

            _dbContext.SaveChanges();
        }
    }
}

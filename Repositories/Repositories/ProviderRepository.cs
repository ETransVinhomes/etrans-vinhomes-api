using Domain.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}

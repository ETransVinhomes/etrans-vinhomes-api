using Domain.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}

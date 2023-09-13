using Domain.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class RouteLocationRepository : GenericRepository<RouteLocation>, IRouteLocationRepository
    {
        public RouteLocationRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class RouteLocationRepository : GenericRepository<RouteLocation>, IRouteLocationRepository
    {
        private readonly AppDbContext _dbContext;
        public RouteLocationRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RouteLocation>> GetByRouteId(Guid routeId)
        => await _dbContext.RouteLocation.Include(x => x.Location).ThenInclude(x => x.LocationType)
                .Where(x => x.RouteId == routeId && x.IsDeleted == false).OrderByDescending(x => x.Index).ToListAsync();
            
        
    }
}

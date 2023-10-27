using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        private readonly AppDbContext _dbContext;
        public RouteRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            return await _dbContext.Route.Include(x => x.RouteLocations)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
                    .ToListAsync();
        }

        public async Task<Route> GetByIdAsync(Guid id)
        => await _dbContext.Route.Include(x => x.RouteLocations)
                                .ThenInclude(x => x.Location)
                                .ThenInclude(x => x.LocationType)
                                .Where(x => x.Id == id).FirstAsync();
                                
    }
}

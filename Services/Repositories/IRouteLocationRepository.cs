using Domain.Entities;

namespace Services.Repositories
{
    public interface IRouteLocationRepository : IGenericRepository<RouteLocation>
    {
        Task<IEnumerable<RouteLocation>> GetByRouteId(Guid routeId);
    }
}

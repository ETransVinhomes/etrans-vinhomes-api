using Domain.Entities;

namespace Services.Repositories
{
    public interface IRouteRepository : IGenericRepository<Route>
    {
        Task<IEnumerable<Route>> GetAllAsync();
        Task<Route> GetByIdAsync(Guid id);
    }
}

using Services.ViewModels.RouteLocationModels;

namespace Services.Services.Interfaces;
public interface IRouteLocationService
{
    Task<IEnumerable<RouteLocationViewModel>> GetAllAsync();
    Task<RouteLocationViewModel> GetByIdAsync(Guid id);
    Task<RouteLocationViewModel> CreateAsync(RouteLocationCreateModel model, Guid routeId);
    Task<RouteLocationViewModel> UpdateAsync(RouteLocationUpdateModel model);
    Task<IEnumerable<RouteLocationViewModel>>CreateRangeAsync(List<RouteLocationCreateModel> models, Guid routeId); 
    Task<bool> DeleteAsync(Guid id); 
    Task<IEnumerable<RouteLocationViewModel>> GetRouteLocByRouteId(Guid routeId);
}
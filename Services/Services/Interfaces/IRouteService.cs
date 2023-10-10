using Services.ViewModels.RouteModels;

namespace Services.Services.Interfaces;
public interface IRouteService
{
    Task<IEnumerable<RouteViewModel>> GetAllAsync();
    Task<RouteViewModel> GetByIdAsync(Guid id);
    Task<RouteViewModel> CreateAsync(RouteCreateModel model);
    Task<RouteViewModel> UpdateAsync(RouteUpdateModel model);
    Task<bool> DeleteAsync(Guid id);
}
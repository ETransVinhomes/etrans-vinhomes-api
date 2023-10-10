using Services.ViewModels.TripModels;
using Services.ViewModels.TripModes;

namespace Services.Services.Interfaces;
public interface ITripService
{
    Task<IEnumerable<TripViewModel>> GetAllAsync();
    Task<TripViewModel> GetByIdAsync(Guid id);
    Task<TripViewModel> CreateAsync(TripCreateModel model);
    Task<TripViewModel> UpdateAsync(TripUpdateModel model);
    Task<bool> DeleteAsync(Guid id); 
}
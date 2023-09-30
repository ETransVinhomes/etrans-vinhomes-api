using Services.ViewModels.VehicleModels;

namespace Services.Services.Interfaces;
public interface IVehicleService
{
    Task<VehicleViewModel> GetVehicleById(Guid id, bool isProviderInclude = false, bool isDriverInclude = false);
    Task<IEnumerable<VehicleViewModel>> GetVehicles(string search = "", bool isDriverInclude = false, bool isProviderInclude = false);
    Task<VehicleViewModel> CreateVehicle(VehicleCreateModel createModel);
    Task<VehicleViewModel> UpdateVehicle(VehicleUpdateModel updateModel);
    Task<bool> DeleteVehicle(Guid id);
}   
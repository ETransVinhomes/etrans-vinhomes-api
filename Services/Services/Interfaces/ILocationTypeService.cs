using Services.ViewModels.LocationType;

namespace Services.Services.Interfaces
{
    public interface ILocationTypeService
    {
        Task<IEnumerable<LocationTypeDTO>> GetAllLocationTypeAsync();
        Task<LocationTypeDTO> GetLocationTypeByIdAsync(Guid id);
    }
}

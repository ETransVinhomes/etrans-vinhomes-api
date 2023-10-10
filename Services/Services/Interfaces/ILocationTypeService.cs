using Services.ViewModels.LocationType;

namespace Services.Services.Interfaces
{
	public interface ILocationTypeService
	{
		Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypeAsync();
		Task<LocationTypeViewModel> GetLocationTypeByIdAsync(Guid id);
		Task<LocationTypeViewModel> CreateLocationTypeAsync(LocationTypeCreateModel model);
	
		Task<LocationTypeViewModel> UpdateLocationTypeAsync(LocationTypeViewModel model);
		Task<bool> DeleteLocationTypeAsync(Guid id);

	}
}

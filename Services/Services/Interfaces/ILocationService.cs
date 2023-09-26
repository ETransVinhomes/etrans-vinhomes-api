using Services.ViewModels.LocationModels;

namespace Services.Services.Interfaces
{
	public interface ILocationService
	{
		Task<IEnumerable<LocationViewModel>> GetAllAsync();
		Task<LocationViewModel> GetByIdAsync(Guid id);

		Task<IEnumerable<LocationViewModel>> FindAsync(string searchString, bool isLocationType = true);
		Task<LocationViewModel> CreateAsync(LocationCreateModel model);
		Task<bool> UpdateAsync(LocationUpdateModel model);
		Task<bool> DeleteAsync(Guid id);
	}
}

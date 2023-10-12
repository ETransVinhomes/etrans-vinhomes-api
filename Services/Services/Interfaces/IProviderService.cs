using Services.ViewModels.ProviderModels;

namespace Services.Services.Interfaces
{
	public interface IProviderService
	{
		Task<IEnumerable<ProviderViewModel>> GetAllAsync(string search = "");
		Task<ProviderViewModel> GetByIdAsync();
		Task<ProviderViewModel> CreateAsync(ProviderCreateModel model);
		Task<bool> UpdateAsync(ProviderUpdateModel model);
		Task<bool> DeleteAsync(Guid id);
	}
}

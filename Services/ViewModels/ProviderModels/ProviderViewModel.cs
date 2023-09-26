using Domain.Entities;

namespace Services.ViewModels.ProviderModels
{
	public class ProviderViewModel
	{
		public Guid Id { get; set; } = default!;
		public string Name { get; set; } = default!;
		public string Address { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Status { get; set; } = default!;
		public ICollection<Vehicle> Vehicles { get; set; } = default!;

	}
}

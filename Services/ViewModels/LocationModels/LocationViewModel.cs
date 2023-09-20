using Services.ViewModels.LocationType;

namespace Services.ViewModels.LocationModels
{
	public class LocationViewModel
	{
		public Guid Id { get; set; } = default!;
		public string Name { get; set; } = default!;
		public LocationTypeViewModel LocationType { get; set; } = default!;

	}
}

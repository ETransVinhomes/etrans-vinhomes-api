namespace Services.ViewModels.LocationModels
{
	public class LocationCreateModel
	{
		public string Name { get; set; } = default!;
		public Guid LocationTypeId { get; set; }
	}
}

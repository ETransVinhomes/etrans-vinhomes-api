using Domains.Entities;

namespace Domain.Entities
{
	public class Location : BaseEntity
	{
		public string Name { get; set; } = default!;
		public Guid LocationTypeId { get; set; } = default!;
		public LocationType LocationType { get; set; } = default!;

		public ICollection<RouteLocation> RouteLocations { get; set; } = default!;
	}
}

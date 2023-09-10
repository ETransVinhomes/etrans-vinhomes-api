using Domains.Entities;

namespace Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = default!;
        public Guid LocationTypeId { get; set; } = default!;
        public LocationType LocationType { get; set; } = default!;

        public ICollection<RouteLocation> StartRouteLocations { get; set; } = default!;
        public ICollection<RouteLocation> EndRouteLocations { get; set; } = default!;
    }
}

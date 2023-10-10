using Domains.Entities;

namespace Domain.Entities
{
    public class RouteLocation : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int Index { get; set; } = 1;
        public bool IsHead { get; set; } = false;
        public Guid? NextRouteLocationId { get; set; } = default!;
        public RouteLocation? NextRouteLocation { get; set; } = default!;
        public Guid LocationId { get; set; } = default!;
        public Location Location { get; set; } = default!;
        public Guid RouteId { get; set; } = default!;
        public Route Route { get; set; } = default!;
        public double Distance { get; set; } = default!;

        public ICollection<RouteLocation> ChildsRouteLocation {get ;set;} = default!;
    }
}

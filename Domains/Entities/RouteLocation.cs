using Domains.Entities;

namespace Domain.Entities
{
    public class RouteLocation : BaseEntity
    {
        public string Name { get; set; } = default!;
        public Guid StartLocationId { get; set; } = default!;
        public Location StartLocation { get; set; } = default!;
        public Guid EndLocationId { get; set; } = default!;
        public Location EndLocation { get; set; } = default!;

        public Guid RouteId { get; set; } = default!;
        public Route Route { get; set; } = default!;
    }
}

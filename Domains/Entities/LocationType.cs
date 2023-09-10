using Domains.Entities;

namespace Domain.Entities
{
    public class LocationType : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<Location> Locations { get; set; } = default!;
    }
}

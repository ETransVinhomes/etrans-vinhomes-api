using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Status { get; set; } = nameof(StatusEnum.Active);
        public Guid ExternalId { get; set; } = default!;

        public ICollection<Route> Routes { get; set; } = default!;
        public ICollection<Vehicle> Vehicles { get; set; } = default!;

    }
}

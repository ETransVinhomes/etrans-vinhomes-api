using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; } = default!;
        public bool? Sex { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public float Rating { get; set; } = 0;
        public string Status { get; set; } = nameof(TransportationStatusEnum.Active);

        public Guid ExternalId { get; set; } = default!;
        public ICollection<Vehicle> Vehicles { get; set; } = default!;


    }
}

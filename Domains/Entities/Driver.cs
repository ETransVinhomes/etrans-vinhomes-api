using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public bool Sex { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public float Rating { get; set; } = default!;
        public string Status { get; set; } = nameof(TransportationStatusEnum.Active);

        public ICollection<Vehicle> Vehicles { get; set; } = default!;


    }
}

using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public bool Sex { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Status { get; set; } = nameof(StatusEnum.Active);

        public ICollection<Order> Orders { get; set; } = default!;
    }
}

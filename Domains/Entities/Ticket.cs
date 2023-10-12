using Domains.Entities;

namespace Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public double Price { get; set; }
        public string? Name { get; set; } = default!;
        public int Quantity {get; set; } = 1;
        public Guid TripId { get; set; } = default!;
        public Trip Trip { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;

    }
}

using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Name { get; set; } = default!;
        public double Total {get; set;} = default!;
        public string Status { get; set; } = nameof(TransactionStatusEnum.Created);
        public Guid CustomerId { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public ICollection<Payment> Payments { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = default!;
    }
}

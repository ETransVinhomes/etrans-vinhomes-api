using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public double Total { get; set; } = default!;
        public string Status { get; set; } = nameof(TransactionStatusEnum.Created);
        public Guid OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}

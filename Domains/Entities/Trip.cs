using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Policy { get; set; } = default!;
        public double Distance { get; set; } = default!;
        public int SeatRemain { get; set; } = default!;
        public float Rating { get; set; } = 0;
        public double Price { get; set; } = 0;
        public Guid RouteId { get; set; } = default!;
        public Route Route { get; set; } = default!;
        public Guid VehicleId { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public string Status { get; set; } = nameof(TripStatusEnum.Active);
        //public DateTime StartedDate { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = default!;
    }
}

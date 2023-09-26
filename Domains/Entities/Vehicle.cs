using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
	public class Vehicle : BaseEntity
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int TotalSeat { get; set; } = 0;
		public string LicensePlate { get; set; } = default!;
		public string Status { get; set; } = nameof(TransportationStatusEnum.Active);

		public Driver Driver { get; set; } = default!;
		public Guid DriverId { get; set; } = default!;
		public Provider Provider { get; set; } = default!;
		public Guid? ProviderId { get; set; } = default!;
		public ICollection<Trip> Trips { get; set; } = default!;


	}
}

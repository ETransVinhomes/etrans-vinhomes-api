using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Services.ViewModels.DriverModels;
using Services.ViewModels.ProviderModels;

namespace Services.ViewModels.VehicleModels
{
	public class VehicleViewModel
	{
		public Guid Id { get; set; } = default!;
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int TotalSeat { get; set; } = default!;
		public string LicensePlate { get; set; } = default!;
		public string Status { get; set; } = default!;
		public Guid ProviderId { get; set; } = default!;
		public Guid? DriverId { get; set; } = default!;
		public DriverViewModel? Driver { get; set; } = default!;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels.VehicleModels
{
	public class VehicleUpdateModel : VehicleCreateModel
	{
		public Guid? Id { get; set; }
	}
}

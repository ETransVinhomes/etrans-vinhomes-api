using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels.DriverModels;

namespace Services.Services.Interfaces
{
	public interface IDriverService
	{
		Task<IEnumerable<DriverViewModel>> GetAllDrivers(string search = "");
		Task<DriverViewModel> GetDriverById(Guid id);
		Task<bool> DeleteDriver(Guid id);
		Task<DriverViewModel> UpdateDriver(DriverUpdateModel model);
		Task<DriverViewModel> CreateDriver(DriverCreateModel model);
	}
}

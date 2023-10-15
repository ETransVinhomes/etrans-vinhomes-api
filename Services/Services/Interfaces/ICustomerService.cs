using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels.CustomerModels;

namespace Services.Services.Interfaces
{
	public interface ICustomerService
	{
		Task<IEnumerable<CustomerViewModel>> GetCustomers(string search = "");
		Task<CustomerViewModel> GetCustomerByIdAsync();
		Task<CustomerViewModel> GetCustomerByIdAsync(Guid id);
		Task<bool> DeleteCustomer(Guid id);
		Task<CustomerViewModel> CreateCustomer(CustomerCreateModel model);
		Task<CustomerViewModel> UpdateCustomer(CustomerUpdateModel model);
	}
}

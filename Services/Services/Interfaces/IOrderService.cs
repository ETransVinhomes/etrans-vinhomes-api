using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels.OrderModels;

namespace Services.Services.Interfaces
{
	public interface IOrderService
	{
		Task<IEnumerable<OrderViewModel>> GetByUserIdAsync(Guid customerId);
		Task<OrderViewModel> CreateAsync(OrderCreateModel model);
		Task<OrderViewModel> GetByIdAsync(Guid id);
		Task<bool> DeleteAsync(Guid id);
		Task<OrderViewModel> UpdateAsync(OrderUpdateModel model);

	}
}

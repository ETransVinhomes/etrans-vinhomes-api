using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels.PaymentModels;

namespace Services.Services.Interfaces
{
	public interface IPaymentService
	{
		Task<PaymentViewModel> CreateAsync(PaymentCreateModel model);
		Task<IEnumerable<PaymentViewModel>> GetByOrderId(Guid orderId);
	}
}

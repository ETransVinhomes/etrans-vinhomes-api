using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.Services.Interfaces;
using Services.ViewModels.PaymentModels;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
        public async Task<PaymentViewModel> CreateAsync(PaymentCreateModel model)
        {
            var payment = _mapper.Map<Payment>(model);
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(payment.OrderId, x => x.Tickets) ?? throw new Exception($"Not found Order With id: {payment.OrderId}");

			payment.Total = order.Total;
			await _unitOfWork.PaymentRepository.AddAsync(payment);
			if(await _unitOfWork.SaveChangesAsync())
			{
				foreach(var ticket in order.Tickets)
			{
				var trip = await _unitOfWork.TripRepository.GetByIdAsync(ticket.TripId) ?? throw new Exception($"Trip is not active");
				if(trip.SeatRemain <= ticket.Quantity) 
				{
					throw new Exception($"--> Error: Trip Id: {trip.Id} | TicketId: {ticket.Id} | SeatRemain : {trip.SeatRemain} | Ticket Quantity: {ticket.Quantity}");
				} else 
				{
					trip.SeatRemain -= ticket.Quantity;
				_unitOfWork.TripRepository.Update(trip);
				}
				
			}
			payment.Status = nameof(TransactionStatusEnum.Completed);
			order.Status = nameof(TransactionStatusEnum.Completed);
			_unitOfWork.OrderRepository.Update(order);
			_unitOfWork.PaymentRepository.Update(payment);
			return await _unitOfWork.SaveChangesAsync() ? 
				_mapper.Map<PaymentViewModel>(await _unitOfWork.PaymentRepository.GetByIdAsync(payment.Id))
				: throw new Exception($"--> Error: Create Payment Failed!");
			} else 
			{
				throw new Exception($"--> Error: Create Payment Failed!");
			}
			
        }

        public async Task<IEnumerable<PaymentViewModel>> GetByOrderId(Guid orderId)
        {
            return _mapper.Map<IEnumerable<PaymentViewModel>>(await _unitOfWork.PaymentRepository.FindListByField(x => x.OrderId == orderId));
        }
    }
}

using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.Services.Interfaces;
using Services.ViewModels.OrderModels;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _mapper = mapper;
            _claimsService = claimsService;
            _unitOfwork = unitOfWork;
        }

        public async Task<OrderViewModel> CreateAsync(OrderCreateModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) model.Name = $"GuestTicket";
            var order = _mapper.Map<Order>(model);
             var user = (await _unitOfwork.CustomerRepository.FindByField(x => x.ExternalId == _claimsService.GetCurrentUser))
             ?? throw new Exception($"Not found Customer With External Id: {_claimsService.GetCurrentUser}");
             order.CustomerId = user.Id;
            double sum = 0;
            if (order.Tickets.Count > 0)
            {
                foreach (var ticket in order.Tickets)
                {
                    var trip = await _unitOfwork.TripRepository.GetByIdAsync(ticket.TripId);
                    if (trip is not null)
                    {
                        if (trip.Status == nameof(TransportationStatusEnum.Active))
                        {
                            ticket.Price = trip.Price * ticket.Quantity;
                            await _unitOfwork.TicketRepository.AddAsync(ticket);
                            sum += ticket.Price;
                        } 
                    }
                    else
                        throw new Exception($"Trip is not active or has started already!");
                }
            }
            else throw new Exception($"Can not create Order with no Ticket");
            await _unitOfwork.OrderRepository.AddAsync(order);
            return await _unitOfwork.SaveChangesAsync()
                ? _mapper.Map<OrderViewModel>(await _unitOfwork.OrderRepository.GetByIdAsync(order.Id, x => x.Tickets))
                : throw new Exception("Save Changes Failed!");

        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<OrderViewModel>(await _unitOfwork.OrderRepository.GetByIdAsync(id));

        public async Task<IEnumerable<OrderViewModel>> GetByUserIdAsync(Guid customerId)
            => _mapper.Map<IEnumerable<OrderViewModel>>(await _unitOfwork.OrderRepository.FindListByField(x => x.CustomerId == customerId));

        public async Task<OrderViewModel> UpdateAsync(OrderUpdateModel model)
        {
            var order = await _unitOfwork.OrderRepository.GetByIdAsync(model.Id) ?? throw new Exception($"Not found Order with Id: {model.Id}");
            _mapper.Map(model, order);
            _unitOfwork.OrderRepository.Update(order);
            return await _unitOfwork.SaveChangesAsync()
            ? _mapper.Map<OrderViewModel>(await _unitOfwork.OrderRepository.GetByIdAsync(model.Id))
            : throw new Exception("Save Changes failed!");

        }
    }
}

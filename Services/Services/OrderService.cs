using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.OrderModels;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfwork = unitOfWork;
        }

        public async Task<OrderViewModel> CreateAsync(OrderCreateModel model)
        {
            var order = _mapper.Map<Order>(model);
            await _unitOfwork.OrderRepository.AddAsync(order);
            return await _unitOfwork.SaveChangesAsync() ?
                _mapper.Map<OrderViewModel>(await _unitOfwork.OrderRepository.GetByIdAsync(order.Id))
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

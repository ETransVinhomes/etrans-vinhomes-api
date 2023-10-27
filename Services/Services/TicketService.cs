using AutoMapper;
using Services.Services.Interfaces;
using Services.ViewModels.TicketModels;

namespace Services.Services;
public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<TicketViewModel>> GetAllAsync()
    {
        var result = await _unitOfWork.TicketRepository.GetAllAsync(x => x.Trip);
        return _mapper.Map<IEnumerable<TicketViewModel>>(result);
    }

    public async Task<TicketViewModel> GetByIdAsync(Guid id)
    {
        var result = await _unitOfWork.TicketRepository.GetByIdAsync(id, x => x.Trip) ?? throw new Exception($"--> Error: Not found Ticket with Id: {id}");
        return _mapper.Map<TicketViewModel>(result);
    }

    public async Task<IEnumerable<TicketViewModel>> GetByOrderIdAsync(Guid orderId)
    {
        var result = await _unitOfWork.TicketRepository.FindListByField(x => x.OrderId == orderId, x => x.Trip);
        if(result.Count > 0)
            return _mapper.Map<IEnumerable<TicketViewModel>>(result);
        else throw new Exception($"Not found any tickets with OrderId: {orderId}");
        
    }

    public Task<IEnumerable<TicketViewModel>> GetTicketByUser(Guid id)
    {
        throw new NotImplementedException();
    }
}
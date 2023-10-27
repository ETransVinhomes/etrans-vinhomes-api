using Services.ViewModels.TicketModels;

namespace Services.Services.Interfaces;
public interface ITicketService
{
    Task<IEnumerable<TicketViewModel>> GetAllAsync();
    Task<TicketViewModel> GetByIdAsync(Guid id);
    Task<IEnumerable<TicketViewModel>> GetByOrderIdAsync(Guid orderId);
    
    Task<IEnumerable<TicketViewModel>> GetTicketByUser(Guid id);
    
}
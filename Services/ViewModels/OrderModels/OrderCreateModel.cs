using Services.ViewModels.TicketModels;

namespace Services.ViewModels.OrderModels;
public class OrderCreateModel
{
    public string? Name {get; set;} = default!;
    //public Guid CustomerId {get; set;} = default!;

    public List<TicketCreateModel> Tickets {get; set;} = default!;
    
}
using Domain.Entities;
using RabbitMQ.Client;
using Services.ViewModels.CustomerModels;
using Services.ViewModels.TicketModels;

namespace Services.ViewModels.OrderModels;
public class OrderViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public DateTime CreationDate { get; set; } = default!;
    public double Total { get; set; } = default!;
    public string Status { get; set; } = default!;
    public CustomerViewModel Customer { get; set; } = default!;
    public ICollection<TicketViewModel> Tickets { get; set; } = default!;
}
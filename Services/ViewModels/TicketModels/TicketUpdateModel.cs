using RabbitMQ.Client;

namespace Services.ViewModels.TicketModels;
public class TicketUpdateModel
{
    public Guid Id { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public string Name { get; set; } = default!;

}
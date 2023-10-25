using Services.ViewModels.TripModels;

namespace Services.ViewModels.TicketModels;
public class TicketViewModel
{
    public Guid Id { get; set; } = default!;
    public Guid TripId { get; set; } = default!;
    public TripViewModel Trip { get; set; } = default!;
    public double Price { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid OrderId { get; set; } = default!;
}
namespace Services.ViewModels.TicketModels;
public class TicketCreateModel
{
    public Guid TripId { get; set; } = default!;
    public int Quantity { get; set; } = 1;
    public string? Name { get; set; } = default!;
}

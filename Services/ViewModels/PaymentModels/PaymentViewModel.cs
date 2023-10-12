using System.Runtime.InteropServices;

namespace Services.ViewModels.PaymentModels;
public class PaymentViewModel
{
    public Guid Id { get; set; } = default!;
    public double Total { get; set; } = default!;
    public Guid OrderId { get; set; } = default!;
}
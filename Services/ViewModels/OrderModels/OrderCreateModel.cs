namespace Services.ViewModels.OrderModels;
public class OrderCreateModel
{
    public string? Name {get; set;} = default!;
    public Guid CustomerId {get; set;} = default!;
    
}
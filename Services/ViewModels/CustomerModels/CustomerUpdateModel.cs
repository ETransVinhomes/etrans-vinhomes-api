namespace Services.ViewModels.CustomerModels;
public class CustomerUpdateModel : CustomerCreateModel
{
    public Guid Id { get; set; } = default!;
}

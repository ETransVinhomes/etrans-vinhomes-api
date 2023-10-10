namespace Services.ViewModels.RouteModels;
public class RouteUpdateModel : RouteCreateModel
{
    public Guid Id { get; set; }
    public string Status {get; set;} = default!;
}
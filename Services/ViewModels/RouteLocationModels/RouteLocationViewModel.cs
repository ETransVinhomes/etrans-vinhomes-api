using Services.ViewModels.LocationModels;

namespace Services.ViewModels.RouteLocationModels;
public class RouteLocationViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Index { get; set; } = default!;
    public LocationViewModel Location { get; set; } = default!;

}
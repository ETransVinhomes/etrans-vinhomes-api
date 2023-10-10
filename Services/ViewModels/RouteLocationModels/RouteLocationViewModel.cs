using Services.ViewModels.LocationModels;
using Services.ViewModels.RouteModels;

namespace Services.ViewModels.RouteLocationModels;
public class RouteLocationViewModel 
{
    public Guid Id {get; set;} = default!;
    public string Name {get ;set;} = default!;
    public int Index {get; set;} = default!;
    public LocationViewModel StartLocation {get;set;} = default!;
    public LocationViewModel EndLocation {get; set;} = default!;
    
}
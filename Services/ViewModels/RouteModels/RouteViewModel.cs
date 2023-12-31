using AutoMapper.Execution;
using Domain.Enums;
using Services.ViewModels.ProviderModels;
using Services.ViewModels.RouteLocationModels;

namespace Services.ViewModels.RouteModels;
public class RouteViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Status { get; set; } = nameof(StatusEnum.Active);
    public Guid ProviderId { get; set; }
    public ProviderViewModel Provider { get; set; } = default!;
    public ICollection<RouteLocationViewModel> RouteLocations { get; set; } = default!;
}
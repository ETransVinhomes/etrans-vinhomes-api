using Domain.Entities;
using Services.ViewModels.RouteModels;
using Services.ViewModels.VehicleModels;

namespace Services.ViewModels.TripModels;
public class TripViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public double Distance { get; set; } = default!;
    public int SeatRemain { get; set; } = default!;
    public float Rating { get; set; } = default!;
    public double Price { get; set; } = default!;
    public RouteViewModel Route { get; set; } = default!;
    public VehicleViewModel Vehicle { get; set; } = default!;
    public DateTime StartedDate { get; set; } = default!;
    public string Status { get; set; } = default!;
}
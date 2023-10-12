namespace Services.ViewModels.TripModes;
public class TripCreateModel
{
    public string? Name { get; set; } = default!;
    public string Policy { get; set; } = default!;
    public Guid RouteId { get; set; } = default!;
    public Guid VehicleId { get; set; } = default!;
    public double Price { get; set; } = default!;
    public DateTime StartedDate { get; set; } = default!;
}
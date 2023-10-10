namespace Services.ViewModels.TripModels;
public class TripUpdateModel
{
    public Guid Id { get; set; } = default!;
    public string Policy { get; set; } = default!;
    public float Rating { get; set; } = default!;
    public double Price { get; set; } = default!;
    public string Status { get; set; } = default!;
    public Guid VehicleId { get; set; } = default!;

}
namespace Services.ViewModels.DriverModels;
public class DriverViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool Sex { get; set; } = default!;
    public DateTime DateOfBirth { get; set; } = default!;
    public float Rating { get; set; } = default!;
    public string Status { get; set; } = default!;
    public Guid ExternalId { get; set; } = default!;
}
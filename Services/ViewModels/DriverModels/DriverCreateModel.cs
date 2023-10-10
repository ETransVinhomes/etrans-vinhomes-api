namespace Services.ViewModels.DriverModels;
public class DriverCreateModel
{
    public string Name { get; set; } = default!;
    public bool Sex { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateOfBirth { get; set; } = default!;
}
namespace Services.ViewModels.CustomerModels;
public class CustomerViewModel
{
    public Guid Id { get; set; } = default!;
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public bool? Sex { get; set; } = default!;
    public DateTime? DateOfBirth { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Status { get; set; } = default!;
}
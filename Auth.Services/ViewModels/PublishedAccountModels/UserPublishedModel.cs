namespace Auth.Services.ViewModels.PublishedAccountModels;
public class UserPublishedModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string PhoneNumber {get; set;} = default!;
    public string Event { get; set; } = default!;
    public string Name { get; set; } = default!;
}
using Auth.Domains.Enums;

namespace Auth.Services.ViewModels.PublishedAccountModels;
public class UserDeleteModel
{
    public Guid Id { get; set; } = default!;
    public string Event { get; set; } = nameof(EventType.UserDelete);
    public string Role { get; set; } = default!;
}
using Domain.Enums;

namespace Services.ViewModels.AsyncDataModels;
public class UserDeleteModel
{
    public Guid Id { get; set; } = default!;
    public string Event { get; set; } = nameof(EventType.UserDelete);
    public string Role { get; set; } = default!;
}
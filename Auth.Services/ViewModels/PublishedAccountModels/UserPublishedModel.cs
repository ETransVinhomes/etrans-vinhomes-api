namespace Auth.Services.ViewModels.PublishedAccountModels;
public class UserPublishedModel
{
    public Guid Id {get; set;} 
    public string Email {get ;set;} = default!;
    public string Role {get; set;} = default!;
}
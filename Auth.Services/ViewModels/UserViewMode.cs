namespace Auth.Services.ViewModels;
public class UserViewModel 
{
    public Guid Id {get; set;}
    public string Username {get; set;} = default!;
    public string Email {get ;set;} = default!;
    public string PhoneNumber {get; set; } = default!;
}
namespace Auth.Services.ViewModels.AuthRequestDTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Username { get; set; } = default!;
    }
}

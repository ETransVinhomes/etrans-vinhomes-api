namespace Auth.Services.ViewModels.AuthRequestDTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? RoleName { get; set; } = "CUSTOMER";
    }
}

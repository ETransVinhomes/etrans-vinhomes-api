namespace Auth.Services.ViewModels.AuthResponseDTO
{
    public class UserDTO
    {
        public Guid Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}

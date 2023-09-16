namespace Auth.Services.ViewModels.AuthResponseDTO
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; } = default!;
        public string Token { get; set; } = default!;

    }
}

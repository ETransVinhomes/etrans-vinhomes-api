using Auth.Services.ViewModels;
using Auth.Services.ViewModels.AuthRequestDTO;
using Auth.Services.ViewModels.AuthResponseDTO;

namespace Auth.Services.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AssignRoleASync(string email, string roleName);
        Task<LoginResponseDTO> LoginAsync(string email, string password);
        Task<LoginResponseDTO> LoginAsync(string googleToken);
        
        Task<bool> RegisterAsync(RegisterDTO registerDTO);
        Task<UserViewModel> GetUserByIdAsync(Guid id);


    }
}

using Auth.Domains.Entities;
using Auth.Domains.Enums;
using Auth.Services.AsyncDataServices.Interfaces;
using Auth.Services.Repositories;
using Auth.Services.Services.Interfaces;
using Auth.Services.ViewModels;
using Auth.Services.ViewModels.AuthRequestDTO;
using Auth.Services.ViewModels.AuthResponseDTO;
using Auth.Services.ViewModels.PublishedAccountModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<AppUser> _appUser;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager, IJWTTokenGenerator jwtTokenGenerator, IMessageBusClient messageBusClient, IMapper mapper)
        {
            _authRepository = authRepository;
            _appUser = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _messageBusClient = messageBusClient;
        }
        public async Task<bool> AssignRoleASync(string email, string roleName)
        {
            roleName = roleName.ToUpper();
            var user = await _authRepository.FindUserAsync(x => x.Email == email);
            if (user is not null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new AppRole { Name = roleName }).GetAwaiter().GetResult();

                }
                if (roleName != "ADMIN")
                {
                    _messageBusClient.PublishNewAccount(new UserPublishedModel
                    {
                        Id = user.Id,
                        Email = user.Email!,
                        Role = roleName,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber!,
                        Event = nameof(EventType.UserCreation)
                    });
                }
                await _appUser.AddToRoleAsync(user, roleName);
                return true;
            }
            else throw new Exception("User not found!");
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<UserViewModel>>(await _authRepository.GetAllAsync());

        public async Task<UserViewModel> GetUserByIdAsync(Guid id)
        {
            var result = await _authRepository.GetUserByIdAsync(id);
            return new()
            {
                Email = result.Email!,
                Id = result.Id,
                PhoneNumber = result.PhoneNumber!,
                Name = result.Name
            };
        }
        public Task<LoginResponseDTO> LoginAsync(string googleToken)
        {
            // Todo
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginAsync(string email, string password)
        {
            var user = await _authRepository.FindUserAsync(x => x.Email!.ToLower().Equals(email.ToLower()));
            var isValid = await _appUser.CheckPasswordAsync(user!, password);
            if (user is not null && isValid)
            {
                return new LoginResponseDTO
                {
                    User = new UserDTO
                    {
                        Email = user.Email!,
                        Id = user.Id,
                        PhoneNumber = user.PhoneNumber!
                    },
                    Token = _jwtTokenGenerator.GenerateToken(user, await _appUser.GetRolesAsync(user))
                };
            }
            else
            {
                return new LoginResponseDTO
                {
                    Token = "",
                    User = null!
                };
            }

        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                Name = registerDTO.Name,
                PhoneNumber = registerDTO.PhoneNumber,
                Email = registerDTO.Email,
                NormalizedEmail = registerDTO.Email,
                UserName = "defaultUser" + Random.Shared.NextInt64()

            };
            try
            {
                if(await _authRepository.FindUserAsync(x => x.Email == user.Email) != null) throw new Exception($"--> Error: Duplicate Email: {user.Email}");
                var result = await _appUser.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

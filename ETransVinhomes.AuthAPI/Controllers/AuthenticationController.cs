using Auth.Services.Services.Interfaces;
using Auth.Services.ViewModels.AuthRequestDTO;
using Auth.Services.ViewModels.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ETransVinhomes.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly ResponseModel _response;
		public AuthenticationController(IAuthService authService)
		{
			_authService = authService;
			_response = new();
		}
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO model)
		{
			var result = await _authService.LoginAsync(model.Email, model.Password);
			if (result is not null && !result.Token.IsNullOrEmpty())
			{
				_response.Result = result;
				return Ok(_response);
			}
			else
			{
				return BadRequest("Login failed!");
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO model)
		{
			var result = await _authService.RegisterAsync(model);
			if (result)
			{
				await _authService.AssignRoleASync(model.Email, "CUSTOMER");
				return Ok();
			}
			else
			{
				return BadRequest("Failed");
			}
		}
	}
}

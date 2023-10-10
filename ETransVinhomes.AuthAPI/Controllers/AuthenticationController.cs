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
		public AuthenticationController(IAuthService authService)
		{
			_authService = authService;

		}
		/// <summary>
		/// Login 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO model)
		{
			var result = await _authService.LoginAsync(model.Email, model.Password);
			if (result is not null && !result.Token.IsNullOrEmpty())
			{
			
				return Ok(result);
			}
			else
			{
				return BadRequest("Login failed!");
			}
		}

		
	}
}

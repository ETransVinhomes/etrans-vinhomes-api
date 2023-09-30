using Auth.Services.Services.Interfaces;
using Auth.Services.ViewModels.AuthRequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace ETransVinhomes.AuthAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;
    public UsersController(IAuthService authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Get User By Id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        return Ok(await _authService.GetUserByIdAsync(id));
    }


    /// <summary>
    /// Register new User
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDTO model)
		{
			var result = await _authService.RegisterAsync(model);
			if (result)
			{
				await _authService.AssignRoleASync(model.Email, "CUSTOMER");
				return StatusCode(StatusCodes.Status201Created);
			}
			else
			{
				return BadRequest("Failed");
			}
		}
}
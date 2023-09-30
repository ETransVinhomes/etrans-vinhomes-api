using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels.LocationModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers;

public class LocationController : BaseController
{
	private readonly ILocationService _locationService;
	private readonly ResponseModel _response;
	public LocationController(ILocationService locationService)
	{
		_locationService = locationService;
		_response = new();
	}

	/// <summary>
	/// Get all locations
	/// </summary>
	/// <response code ="200"></response>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] string typeName = "")
	{
		IEnumerable<LocationViewModel> locationList;
		if (string.IsNullOrEmpty(typeName))
		{
			locationList = await _locationService.GetAllAsync();
		}
		else
		{
			locationList = await _locationService.FindAsync(typeName);
		}

		_response.Result = locationList;
		return Ok(_response);
	}

	/// <summary>
	/// Get location by id
	/// </summary>
	/// <param name="id">Guid</param>
	/// <returns></returns>
	/// /// <response code ="200"></response>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var location = await _locationService.GetByIdAsync(id);
		_response.Result = location;
		return Ok(_response);
	}
	/// <summary>
	/// Create location
	/// </summary>
	/// <param name="model">LocationCreateModel</param>
	/// <returns>ResponseModel</returns>
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] LocationCreateModel model)
	{
		var createdLocation = await _locationService.CreateAsync(model);
		_response.Result = createdLocation;
		return StatusCode(StatusCodes.Status201Created, _response);
	}
	/// <summary>
	/// Delete Location
	/// </summary>
	/// <param name="id">Guid</param>
	/// <returns>No Content - 204</returns>
	/// <response code="204"></response>
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await _locationService.DeleteAsync(id);
		return NoContent();
	}

	/// <summary>
	/// Update Location 
	/// </summary>
	/// <param name="model">LocationUpdateModel</param>
	/// <returns>No Content - 204</returns>
	[HttpPut]
	public async Task<IActionResult> Update([FromBody] LocationUpdateModel model)
	{
		await _locationService.UpdateAsync(model);
		return NoContent();
	}
	

}


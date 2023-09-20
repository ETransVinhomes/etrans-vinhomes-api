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
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var locationList = await _locationService.GetAllAsync();
		_response.Result = locationList;
		return Ok(_response);
	}

	/// <summary>
	/// Get location by id
	/// </summary>
	/// <param name="id">Guid</param>
	/// <returns></returns>
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
		return Ok(_response);
	}
	/// <summary>
	/// Delete Location
	/// </summary>
	/// <param name="id">Guid</param>
	/// <returns>No Content - 204</returns>
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


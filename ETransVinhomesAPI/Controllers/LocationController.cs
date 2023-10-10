using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels.LocationModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers;

public class LocationController : BaseController
{
	private readonly ILocationService _locationService;

	public LocationController(ILocationService locationService)
	{
		_locationService = locationService;
	}

	/// <summary>
	/// Get all locations
	/// </summary>
	/// <response code ="200"></response>
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var result = await _locationService.GetAllAsync();
		return Ok(result);
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

		return Ok(location);
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
	
	[HttpPost]
	public async Task<IActionResult> Create([FromBody]List<LocationCreateModel> models)
	{
		return await _locationService.CreateRangeAsync(models) ? StatusCode(StatusCodes.Status201Created) : BadRequest("Create Failed!");
		
	}

}


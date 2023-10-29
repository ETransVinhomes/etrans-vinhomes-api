using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
	[HttpGet]
	[EnableQuery]
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
	[EnableQuery]
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
	[ProducesResponseType((int)HttpStatusCode.NoContent)]
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
	[ProducesResponseType((int)HttpStatusCode.NoContent)]
	[HttpPut]
	public async Task<IActionResult> Update([FromBody] LocationUpdateModel model)
	{
		await _locationService.UpdateAsync(model);
		return NoContent();
	}
	/// <summary>
	/// Create New Location
	/// </summary>
	/// <param name="models"></param>
	/// <returns></returns>
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.Created)]
	public async Task<IActionResult> Create([FromBody]List<LocationCreateModel> models)
	{
		return await _locationService.CreateRangeAsync(models) ? StatusCode(StatusCodes.Status201Created) : BadRequest("Create Failed!");
		
	}

}


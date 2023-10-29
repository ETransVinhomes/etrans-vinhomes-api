using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Services.Interfaces;
using Services.ViewModels.LocationType;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers
{
	[Route("api/location-types")]
	public class LocationTypeController : BaseController
	{
		private readonly ILocationTypeService _locationTypeService;

		public LocationTypeController(ILocationTypeService locationTypeService)
		{
			_locationTypeService = locationTypeService;

		}

		/// <summary>
		/// Get all Location Types
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidDataException"></exception>
		/// <response code="200"></response>
		[HttpGet]
		[EnableQuery]
		public async Task<IActionResult> GetAll()
		{
			var locationTypeList = await _locationTypeService.GetAllLocationTypeAsync();
			if (locationTypeList.Count() > 0)
			{

				return Ok(locationTypeList);
			}
			else throw new InvalidDataException("LocationType is null");

		}

		/// <summary>
		/// Get Location type by id
		/// </summary>
		/// <param name="id">Guid</param>
		/// <returns>Ok - 200 - ResponseModel object</returns>
		/// <exception cref="Exception">Not found</exception>
		/// <response code="200"></response>
		/// <response code="400"></response>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var locationType = await _locationTypeService.GetLocationTypeByIdAsync(id);
			return locationType != null ? Ok(locationType) : throw new Exception("Not found!");
		}
		/// <summary>
		/// Create New Location Type
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<IActionResult> Create([FromBody] LocationTypeCreateModel model)
		{
			var result = await _locationTypeService.CreateLocationTypeAsync(model);
			if (result is not null)
			{

				return CreatedAtRoute(nameof(GetById), new { id = result.Id }, result);
			}
			else throw new Exception("Create failed!");
		}

		/// <summary>
		/// Delete Location Type
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[ProducesResponseType((int) HttpStatusCode.NoContent)]
		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var isDelete = await _locationTypeService.DeleteLocationTypeAsync(id);
			if (isDelete)
			{
				return NoContent();
			}
			else
			{
				throw new Exception("Delete Failed!");
			}
		}

	}
}

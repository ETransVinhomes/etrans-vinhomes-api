using Microsoft.AspNetCore.Mvc;
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
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		/// <response code="201">If create successfully</response>
		/// <response code="400"></response>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] LocationTypeCreateModel model)
		{
			var result = await _locationTypeService.CreateLocationTypeAsync(model);
			if (result is not null)
			{

				return CreatedAtRoute(nameof(GetById), new { id = result.Id }, result);
			}
			else throw new Exception("Create failed!");
		}

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

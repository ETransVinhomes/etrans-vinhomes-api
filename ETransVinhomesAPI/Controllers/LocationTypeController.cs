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
		private readonly ResponseModel _response;
		public LocationTypeController(ILocationTypeService locationTypeService)
		{
			_locationTypeService = locationTypeService;
			_response = new ResponseModel();
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var locationTypeList = await _locationTypeService.GetAllLocationTypeAsync();
			if (locationTypeList.Count() > 0)
			{
				_response.Result = locationTypeList;
				return Ok(_response);
			}
			else throw new InvalidDataException("LocationType is null");

		}

		/// <summary>
		/// Get Location type by id
		/// </summary>
		/// <param name="id">Guid</param>
		/// <returns>Ok - 200 - ResponseModel object</returns>
		/// <exception cref="Exception">Not found</exception>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var locationType = await _locationTypeService.GetLocationTypeByIdAsync(id);
			return locationType != null ? Ok(_response.Result = locationType) : throw new Exception("Not found!");
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] LocationTypeCreateModel model)
		{
			var result = await _locationTypeService.CreateLocationTypeAsync(model);
			if (result is not null)
			{
				_response.Result = result;
				return Ok(_response);
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

using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI.Controllers
{

    public class LocationTypeController : BaseController
    {
        private readonly ILocationTypeService _locationTypeService;
        public LocationTypeController(ILocationTypeService locationTypeService)
        {
            _locationTypeService = locationTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _locationTypeService.GetAllLocationTypeAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _locationTypeService.GetLocationTypeByIdAsync(id));
        }

    }
}

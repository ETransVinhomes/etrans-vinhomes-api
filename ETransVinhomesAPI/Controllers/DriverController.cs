using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.AsyncDataServices.Interfaces;
using Services.Services.Interfaces;
using Services.ViewModels.DriverModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers;
public class DriverController : BaseController
{
    private readonly IDriverService _driverService;


    public DriverController(IDriverService driverService)
    {

        _driverService = driverService;

    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _driverService.GetAllDrivers();

        return Ok(result.AsQueryable());
    }
    [Authorize(Roles = nameof(RoleEnum.DRIVER))]
    [Route("details")]
    [HttpGet]
    public async Task<IActionResult> GetById()
    {
        var result = await _driverService.GetDriverByIdAsync();

        return Ok(result);

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _driverService.GetDriverByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DriverCreateModel model)
    {
        var result = await _driverService.CreateDriver(model);
        if (result is not null)
        {

            return StatusCode(StatusCodes.Status201Created, result);
        }
        else throw new Exception("Created Driver Failed!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] DriverUpdateModel model, [FromRoute] Guid id)
    {
        model.Id = id;
        var result = await _driverService.UpdateDriver(model);
        if (result is not null)
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        }
        else throw new Exception($"Updated Driver With Id: {model.Id} Failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _driverService.DeleteDriver(id);
        if (result)
        {
            return NoContent();
        }
        else
        {
            throw new Exception($"Delete Driver With Id: {id} Failed!");
        }
    }
}
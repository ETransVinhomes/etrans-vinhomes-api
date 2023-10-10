using Microsoft.AspNetCore.Mvc;
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var result = await _driverService.GetDriverById(id);   
        if(result is not null) 
        {

            return Ok(result);
        } else throw new Exception($"Not found Driver With Id: {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DriverCreateModel model) 
    {
        var result = await _driverService.CreateDriver(model);
        if(result is not null) 
        {

            return StatusCode(StatusCodes.Status201Created, result);
        } else throw new Exception("Created Driver Failed!");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DriverUpdateModel model)
    {
        var result = await _driverService.UpdateDriver(model);
        if(result is not null) 
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        } else throw new Exception($"Updated Driver With Id: {model.Id} Failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        var result = await _driverService.GetDriverById(id);
        if(result is not null)
        {
           return NoContent();
        } else 
        {
            throw new Exception($"Delete Driver With Id: {id} Failed!");
        }
    }
}
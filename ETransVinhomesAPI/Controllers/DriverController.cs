using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels.DriverModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers;
public class DriverController : BaseController
{
    private readonly IDriverService _driverService;
    private readonly ResponseModel _response;
    public DriverController(IDriverService driverService)
    {
        _response = new();
        _driverService = driverService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string search = "") 
    {
        var result = await _driverService.GetAllDrivers(search);
        _response.Result = result;
        return Ok(_response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var result = await _driverService.GetDriverById(id);   
        if(result is not null) 
        {
            _response.Result = result;
            return Ok(_response);
        } else throw new Exception($"Not found Driver With Id: {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DriverCreateModel model) 
    {
        var result = await _driverService.CreateDriver(model);
        if(result is not null) 
        {
            _response.Result = result;
            return StatusCode(StatusCodes.Status201Created, _response);
        } else throw new Exception("Created Driver Failed!");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DriverUpdateModel model)
    {
        var result = await _driverService.UpdateDriver(model);
        if(result is not null) 
        {
            _response.Result = result;
            return StatusCode(StatusCodes.Status204NoContent, result);
        } else throw new Exception($"Updated Driver With Id: {model.Id} Failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        var result = await _driverService.GetDriverById(id);
        if(result is not null)
        {
            _response.Result = result;
            _response.Message = $"Delete Driver with Id: {id} successfully!";
            return StatusCode(StatusCodes.Status204NoContent, _response);
        } else 
        {
            throw new Exception($"Delete Driver With Id: {id} Failed!");
        }
    }
}
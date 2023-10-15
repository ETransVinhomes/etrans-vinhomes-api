using System.Globalization;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels.ResponseModels;
using Services.ViewModels.VehicleModels;

namespace ETransVinhomesAPI.Controllers;

public class VehicleController : BaseController
{
    private readonly IVehicleService _vehicleService;
    private ResponseModel _response;
    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        _response = new();
    }
/// <summary>
/// Get All Vehicles
/// </summary>
/// <param name="search">optional</param>
/// <param name="driver">optional-Get Driver or not</param>
/// <param name="provider">optional-Get Provider or not</param>
/// <returns></returns>
/// <response code="200">Get Response Model with List of Vehicles</response>
    [HttpGet]
    public async Task<IActionResult> GetVehicles([FromQuery] string search = "",[FromQuery] bool driver = false,[FromQuery] bool provider = false)
    {
        var result = await _vehicleService.GetVehicles(search, driver, provider);
        _response.Result = result;
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var result = await _vehicleService.GetVehicleById(id);
        if(result is not null)
        {
            _response.Result = result;
            return Ok(_response);
        } else throw new Exception($"Not found Vehicle with Id: {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]VehicleCreateModel model) 
    {
        var result = await _vehicleService.CreateVehicle(model);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody]VehicleUpdateModel model, [FromQuery] Guid id)
    {
        model.Id = id;
        var result =  await _vehicleService.UpdateVehicle(model);
        if(result is not null)
        {
            _response.Result = result;
           return NoContent();
        } else {
            throw new Exception("Update Failed!");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _vehicleService.DeleteVehicle(id);
        if(result)
        {
            return NoContent();
        } else throw new Exception("Delete Failed!");
    }
}
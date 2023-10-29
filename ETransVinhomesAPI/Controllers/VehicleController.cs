using System.Globalization;
using System.Net;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels.ResponseModels;
using Services.ViewModels.VehicleModels;

namespace ETransVinhomesAPI.Controllers;

public class VehicleController : BaseController
{
    private readonly IVehicleService _vehicleService;
    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }
    /// <summary>
    /// Get All Vehicles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetVehicles()
    {
        var result = await _vehicleService.GetVehicles("");
        return Ok(result.AsQueryable());
    }
    /// <summary>
    /// Get Vehicle By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var result = await _vehicleService.GetVehicleById(id);
        if (result is not null)
        {
            return Ok(result);
        }
        else throw new Exception($"Not found Vehicle with Id: {id}");
    }


    /// <summary>
    /// Create Vehicle
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create([FromBody] VehicleCreateModel model)
    {
        var result = await _vehicleService.CreateVehicle(model);
        return StatusCode(StatusCodes.Status201Created);
    }
    /// <summary>
    /// Update Vehicle
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] VehicleUpdateModel model, [FromRoute] Guid id)
    {
        model.Id = id;
        var result = await _vehicleService.UpdateVehicle(model);
        if (result is not null)
        {
            return NoContent();
        }
        else
        {
            throw new Exception("Update Failed!");
        }
    }   
    /// <summary>
    /// Delete Vehicle
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _vehicleService.DeleteVehicle(id);
        if (result)
        {
            return NoContent();
        }
        else throw new Exception("Delete Failed!");
    }
}
using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
    /// <summary>
    /// Get All Drivers
    /// </summary>
    [EnableQuery]
    [Authorize($"{nameof(RoleEnum.ADMIN)}, {nameof(RoleEnum.PROVIDER)}")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _driverService.GetAllDrivers();

        return Ok(result.AsQueryable());
    }

    /// <summary>
    /// Get Driver Profile - Get Driver By JWT Token
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = nameof(RoleEnum.DRIVER))]
    [Route("details")]
    [HttpGet]
    [EnableQuery]

    public async Task<IActionResult> GetById()
    {
        var result = await _driverService.GetDriverByIdAsync();

        return Ok(result);

    }

    /// <summary>
    /// Get Driver By Id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [EnableQuery]
    [Authorize($"{nameof(RoleEnum.ADMIN)}, {nameof(RoleEnum.PROVIDER)}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _driverService.GetDriverByIdAsync(id));
    }

    /// <summary>
    /// Create Driver
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [HttpPost]
    [Authorize($"{nameof(RoleEnum.ADMIN)}")]
    public async Task<IActionResult> Create([FromBody] DriverCreateModel model)
    {
        var result = await _driverService.CreateDriver(model);
        if (result is not null)
        {

            return StatusCode(StatusCodes.Status201Created, result);
        }
        else throw new Exception("Created Driver Failed!");
    }
    /// <summary>
    /// Update Driver
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut("{id}")]
    [Authorize($"{nameof(RoleEnum.ADMIN)}")]
    public async Task<IActionResult> Update([FromBody] DriverUpdateModel model, [FromRoute] Guid id)
    {
        model.Id = id;
        var result = await _driverService.UpdateDriver(model);
        if (result is not null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        else throw new Exception($"Updated Driver With Id: {model.Id} Failed!");
    }
    /// <summary>
    /// Delete Driver
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{id}")]
    [Authorize($"{nameof(RoleEnum.ADMIN)}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
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
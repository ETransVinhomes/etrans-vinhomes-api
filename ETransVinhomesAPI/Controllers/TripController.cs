using System.Dynamic;
using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Services.Interfaces;
using Services.ViewModels.RatingModels;
using Services.ViewModels.TripModels;
using Services.ViewModels.TripModes;

namespace ETransVinhomesAPI.Controllers;
public class TripController : BaseController
{
    private readonly ITripService _tripService;
    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }


    /// <summary>
    ///  Get All Trips
    /// </summary>
    /// <returns></returns>
    [HttpGet] [EnableQuery]
    public async Task<IActionResult> Get()
    {
        var result = await _tripService.GetAllAsync();
        return Ok(result.AsQueryable());
    }

    /// <summary>
    /// Create Trip
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [EnableQuery]
    [ProducesResponseType(typeof(TripViewModel),(int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create([FromBody] TripCreateModel model)
    {
        var result = await _tripService.CreateAsync(model);
        if (result is not null)
            return StatusCode(StatusCodes.Status201Created, result);
        else throw new Exception("Create Failed!");
    }
    /// <summary>
    /// Update Trip
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TripUpdateModel model)
    {
        await _tripService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Delete Trip
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(RoleEnum.PROVIDER))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _tripService.DeleteAsync(id);
        return NoContent();
    }
    /// <summary>
    /// Get Trip By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<IActionResult> GetById(Guid id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        if (trip is not null)
        {
            return Ok(trip);
        }
        else throw new Exception($"Not found Trip with Id: {id}");
    }

    /// <summary>
    /// Finish Trip (For Driver)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(RoleEnum.DRIVER))]
    [HttpPatch("{id}")]
    public async Task<IActionResult> FinishTrip([FromRoute] Guid id)
    {
        await _tripService.FinishTrip(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Rating([FromBody] RatingCreateModel model, Guid id)
    {
        if(await _tripService.RatingAsync(id, model))
        {
            return NoContent();
        } else {
            throw new Exception("Rating Failed!");
        }
    }



}
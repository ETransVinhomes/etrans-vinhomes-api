using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
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

    [HttpGet]
    public async Task<IActionResult> Get() 
    {
        var result = await _tripService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TripCreateModel model)
    {
        var result = await _tripService.CreateAsync(model);
        if(result is not null)
        return StatusCode(StatusCodes.Status201Created, result);
        else throw new Exception("Create Failed!");
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TripUpdateModel model)
    {
        await _tripService.UpdateAsync(model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        await _tripService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        if(trip is not null)
        {
            return Ok(trip);
        } else throw new Exception($"Not found Trip with Id: {id}");    
    }


}
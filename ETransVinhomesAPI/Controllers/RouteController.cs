using System.Net;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels.RouteLocationModels;
using Services.ViewModels.RouteModels;

namespace ETransVinhomesAPI.Controllers;
public class RouteController : BaseController 
{
    private readonly IRouteService _routeService;
    private readonly IRouteLocationService _routeLocationService;
    public RouteController(IRouteService routeService, IRouteLocationService routeLocationService)
    {
        _routeService = routeService;
        _routeLocationService = routeLocationService;
    }

/// <summary>
/// Get All Routes
/// </summary>
/// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _routeService.GetAllAsync();
        if(result.Count() > 0)
            return Ok(result);
        else throw new Exception("!List Empty");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var result = await _routeService.GetByIdAsync(id);
        if(result is not null)
            return Ok(result);
        else throw new Exception($"Not Found Route with Id: {id}");
    }
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RouteCreateModel model)
    {
        var result = await _routeService.CreateAsync(model);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    /// <summary>
    /// Update Route
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] RouteUpdateModel model)
    {
        var result = await _routeService.UpdateAsync(model);
        return StatusCode(StatusCodes.Status204NoContent);
        
    }
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        var result = await _routeService.DeleteAsync(id);
        if(result)
            return NoContent();
        else throw new Exception("Delete Failed!");
    }

    [ProducesResponseType(typeof(RouteLocationViewModel), (int)HttpStatusCode.Created)]
    [HttpPost("{id}/route-locations")]
    public async Task<IActionResult> CreateRouteLocation([FromBody] List<RouteLocationCreateModel> models, [FromRoute]Guid id)
    {
        var result = await _routeLocationService.CreateRangeAsync(models, id);
        if(result is not null)
        {
            return StatusCode(StatusCodes.Status201Created, result);
        } else 
        {
            return BadRequest($"Create Route Location Failed!");
        }
    }


}
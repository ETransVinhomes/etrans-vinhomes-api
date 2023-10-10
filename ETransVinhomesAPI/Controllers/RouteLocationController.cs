using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI.Controllers;
[ApiController]
[Route("api/routes/{routeId}/route-locations")]
public class RouteLocationController : ControllerBase 
{
    private readonly IRouteLocationService _routeLocationService;
    public RouteLocationController(IRouteLocationService routeLocationService)
    {
        _routeLocationService = routeLocationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid routeId)
    {
        var result = await _routeLocationService.GetRouteLocByRouteId(routeId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid routeId, Guid id)
    {
        await _routeLocationService.DeleteAsync(id);
        return NoContent();
    }
}
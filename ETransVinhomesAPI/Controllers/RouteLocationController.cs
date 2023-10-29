using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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

    /// <summary>
    ///  Get Route Locations Details By RouteId
    /// </summary>
    /// <param name="routeId">Guid</param>
    /// <returns></returns>
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> Get(Guid routeId)
    {
        var result = await _routeLocationService.GetRouteLocByRouteId(routeId);
        return Ok(result.AsQueryable());
    }

    /// <summary>
    /// Delete Route Location
    /// </summary>
    /// <param name="routeId">Guid</param>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [Authorize(Roles = $"{nameof(RoleEnum.PROVIDER)}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid routeId, Guid id)
    {
        await _routeLocationService.DeleteAsync(id);
        return NoContent();
    }
}
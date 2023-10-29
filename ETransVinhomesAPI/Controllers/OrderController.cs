using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Services.Interfaces;
using Services.ViewModels.OrderModels;

namespace ETransVinhomesAPI.Controllers;
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [Route("/api/users/{id}/[controller]s")]
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetOrdersByUserId(Guid id)
    {
        var result = await _orderService.GetByUserIdAsync(id);
        return Ok(result.AsQueryable());
    }
    /// <summary>
    /// Get Order By OrderId
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return Ok(result);
    }
    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [Authorize(Roles = nameof(RoleEnum.CUSTOMER))]
    public async Task<IActionResult> Post(OrderCreateModel model)
    {
        var result = await _orderService.CreateAsync(model);
        if (result is not null)
        {
            return StatusCode(StatusCodes.Status201Created, result);
        }
        else throw new Exception($"Create Failed!");
    }

    /// <summary>
    /// Delete Order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Authorize(Roles = $"{nameof(RoleEnum.ADMIN)}, {nameof(RoleEnum.CUSTOMER)}")]
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _orderService.DeleteAsync(id);
        if (result)
            return NoContent();
        else throw new Exception("Delete Failed!");
    }

    /// <summary>
    /// Update Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Authorize(Roles = $"{nameof(RoleEnum.ADMIN)}, {nameof(RoleEnum.CUSTOMER)}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrderUpdateModel model)
    {
        var result = await _orderService.UpdateAsync(model);
        if (result is not null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        else throw new Exception($"Update Failed!");
    }

}
using Microsoft.AspNetCore.Mvc;
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
    [Route("/api/users/{id}/[controller]")]
    [HttpGet]
    public async Task<IActionResult> GetOrdersByUserId(Guid id)
    {
        var result = await _orderService.GetByUserIdAsync(id);
        return Ok(result.AsQueryable());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost] 
    public async Task<IActionResult> Post(OrderCreateModel model) 
    {
        var result = await _orderService.CreateAsync(model);
        if(result is not null)
        {
            return StatusCode(StatusCodes.Status201Created, result);
        } else throw new Exception($"Create Failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        var result = await _orderService.DeleteAsync(id);
        if(result)
            return NoContent();
        else throw new Exception("Delete Failed!");
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrderUpdateModel model)
    {
        var result = await _orderService.UpdateAsync(model);
        if(result is not null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        } else throw new Exception($"Update Failed!");
    }
    
}
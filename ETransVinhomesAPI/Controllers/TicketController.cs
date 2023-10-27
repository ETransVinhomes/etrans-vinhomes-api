using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI.Controllers;
public class TicketController : BaseController
{
    private readonly ITicketService _ticketService;
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [Route("/api/users/{id}/tickets")]
    [HttpGet]
    public async Task<IActionResult> GetByUserId(Guid id)
    {
        var result = await _ticketService.GetTicketByUser(id);
        return Ok(result);
    }

    /// <summary>
    /// Get All Tickets By OrderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [Route("/api/orders/{orderId}/tickets")]
    [HttpGet]
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        var result = await _ticketService.GetByOrderIdAsync(orderId);
        return Ok(result);
    }

    [Route("/api/tickets/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _ticketService.GetByIdAsync(id);
        return Ok(result);
    }


}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI.Controllers;
public class TicketController : BaseController
{
    private readonly ITicketService _ticketService;
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    /// <summary>
    /// Get All Ticket By UserId
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [Route("/api/users/{id}/tickets")]
    [HttpGet] [EnableQuery]
    public async Task<IActionResult> GetByUserId(Guid id)
    {
        var result = await _ticketService.GetTicketByUser(id);
        return Ok(result.AsQueryable());
    }

    /// <summary>
    /// Get All Tickets By OrderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [Route("/api/orders/{orderId}/tickets")]
    [HttpGet] [EnableQuery]
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        var result = await _ticketService.GetByOrderIdAsync(orderId);
        return Ok(result.AsQueryable());
    }

    /// <summary>
    ///  Get Ticket By Id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [Route("/api/tickets/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _ticketService.GetByIdAsync(id);
        return Ok(result);
    }


}
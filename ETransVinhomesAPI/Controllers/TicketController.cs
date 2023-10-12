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

    [Route("/api/tickets/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _ticketService.GetByIdAsync(id);
        return Ok(result);
    }

    
} 
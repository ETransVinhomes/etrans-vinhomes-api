using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels.OrderModels;
using Services.ViewModels.PaymentModels;

namespace ETransVinhomesAPI.Controllers;

[ApiController]
[Route("api/orders/{orderId}/[controller]s")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [HttpPost]
    public async Task<IActionResult> CreatePayment(Guid orderId)
    {
        var result = await _paymentService.CreateAsync(new PaymentCreateModel { OrderId = orderId });
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Get All Payments Of Order 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// 
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet]
    public async Task<IActionResult> GetPaymentByOrder(Guid orderId)
    {
        var result = await _paymentService.GetByOrderId(orderId);
        if (result.Count() > 0)
            return Ok(result);
        else return BadRequest($"--> Error: Payment List of Order is Empty. OrderId: {orderId}");
    }
}
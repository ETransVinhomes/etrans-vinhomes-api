using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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

    /// <summary>
    /// Create Payment -- Complete Order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [Authorize(Roles = $"{nameof(RoleEnum.CUSTOMER)}, {nameof(RoleEnum.ADMIN)}")]
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
    [EnableQuery]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet]
    public async Task<IActionResult> GetPaymentByOrder(Guid orderId)
    {
        var result = await _paymentService.GetByOrderId(orderId);
        if (result.Count() > 0)
            return Ok(result.AsQueryable());
        else return BadRequest($"--> Error: Payment List of Order is Empty. OrderId: {orderId}");
    }
}
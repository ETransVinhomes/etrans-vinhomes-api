using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Services.Interfaces;
using Services.ViewModels.CustomerModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers;
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;

    }
    /// <summary>
    /// Get All Customer
    /// </summary>
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _customerService.GetCustomers();

        return Ok(result.AsQueryable());
    }

    /// <summary>
    /// Get Customer Detail - Id Get on JWT Token
    /// </summary>
    [Route("details")]
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetById()
    {
        var result = await _customerService.GetCustomerByIdAsync();

        return Ok(result);

    }


    /// <summary>
    /// Get Customer By Id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns></returns>
    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok(await _customerService.GetCustomerByIdAsync(id));
    }



    /// <summary>
    /// Create Customer
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(((int)HttpStatusCode.Created))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerCreateModel model)
    {
        var result = await _customerService.CreateCustomer(model);
        if (result is not null)
        {

            return StatusCode(StatusCodes.Status201Created, result);
        }
        else
        {
            throw new Exception("Create failed!");
        }
    }


    /// <summary>
    /// Update Customer -- Value automatically Update at AuthService
    /// </summary>
    [Authorize(Roles = nameof(RoleEnum.CUSTOMER))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateModel model, [FromRoute] Guid id)
    {
        var result = await _customerService.UpdateCustomer(model, id);
        if (result is not null)
        {

            return StatusCode(StatusCodes.Status204NoContent);
        }
        throw new Exception("Update failed!");
    }
    /// <summary>
    /// Delete Customer -- Value automatically Update at AuthService
    /// </summary>
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _customerService.DeleteCustomer(id);
        return NoContent();

    }

}
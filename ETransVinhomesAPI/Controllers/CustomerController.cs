using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _customerService.GetCustomers();

        return Ok(result.AsQueryable());
    }

    [Route("details")]
    [HttpGet]
    public async Task<IActionResult> GetById() 
    {
        var result = await _customerService.GetCustomerById();

            return Ok(result);
        
    }

    // Cancel
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerCreateModel model) 
    {
        var result = await _customerService.CreateCustomer(model);
        if(result is not null)
        {

            return StatusCode(StatusCodes.Status201Created, result);
        } else {
            throw new Exception("Create failed!");
        }
    }

    [Authorize(Roles = nameof(RoleEnum.CUSTOMER))]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateModel model, [FromRoute]Guid id)
    {
        model.Id = id;
        var result = await _customerService.UpdateCustomer(model);
        if(result is not null)
        {

            return StatusCode(StatusCodes.Status204NoContent, result);
        }
        throw new Exception("Update failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
       await _customerService.DeleteCustomer(id);
        return NoContent();
        
    }

}
using System.Net;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Identity.Client;
using Services.Services.Interfaces;
using Services.ViewModels.ProviderModels;
using Services.ViewModels.ResponseModels;

namespace ETransVinhomesAPI.Controllers
{
	public class ProviderController : BaseController
	{
		private readonly IProviderService _providerService;
		public ProviderController(IProviderService providerService)
		{
			_providerService = providerService;

		}

		/// <summary>
		/// Get all providers
		/// </summary>
		/// <returns></returns>
		/// <response code="200"></response>
		/// <response code="400">Error. Detail at</response>
		[HttpGet]
		[EnableQuery]
		public async Task<IActionResult> GetAll()
		{
			var result = await _providerService.GetAllAsync();

			if (result.Count() > 0)
			{

				return Ok(result.AsQueryable());
			}
			else throw new Exception("List is emptied! Please add provider First!");
		}
		/// <summary>
		///  Get Provider By Id
		/// </summary>
		/// <param name="id">Guid</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		[EnableQuery]
		
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var result = await _providerService.GetByIdAsync(id);
			return Ok(result);
		}

		/// <summary>
		/// Get Provider Profile -- By JWT Token
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[Route("details")][HttpGet][EnableQuery]
		public async Task<IActionResult> GetById()
		{
			var result = await _providerService.GetByIdAsync();
			if (result is not null)
			{

				return Ok(result);
			}
			else throw new Exception("Not found!");
		}
		/// <summary>
		/// Create provider
		/// </summary>
		/// <remarks>
		/// Sample Request model: 
		/// 
		///		POST /api/provider
		///		{
		///			"Name" : "string",
		///			"Address" : "string", 
		///			"PhoneNumber" : "string"
		///		}
		///		
		/// </remarks>
		/// <param name="model"></param>
		[Authorize(Roles = nameof(RoleEnum.ADMIN))]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ProviderCreateModel model)
		{
			var result = await _providerService.CreateAsync(model);
			if (result is not null)
			{

				return StatusCode(StatusCodes.Status201Created, result);
			}
			else throw new Exception("Create failed!");
		}
		/// <summary>
		/// Update Provider -- Automatically update at AuthService
		/// </summary>
		/// <param name="model"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[Authorize(Roles = $"{nameof(RoleEnum.PROVIDER)}, {nameof(RoleEnum.ADMIN)}")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromBody] ProviderUpdateModel model, [FromRoute] Guid id)
		{
			model.Id = id;
			var result = await _providerService.UpdateAsync(model);
			if (result)
			{
				return NoContent();
			}
			else throw new Exception("Update failed!");
		}
		/// <summary>
		/// Delete provider 
		/// </summary>
		/// <param name="id">Guid</param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpDelete("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[Authorize(Roles = $"{nameof(RoleEnum.PROVIDER)}, {nameof(RoleEnum.ADMIN)}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _providerService.DeleteAsync(id);
			if (result)
			{
				return NoContent();
			}
			else throw new Exception("Delete failed!");
		}
	}
}

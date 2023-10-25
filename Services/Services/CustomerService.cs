using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.AsyncDataServices.Interfaces;
using Services.Services.Interfaces;
using Services.ViewModels.AsyncDataModels;
using Services.ViewModels.CustomerModels;

namespace Services.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMessageBusClient _messageBusClient;
		private readonly IMapper _mapper;
		private readonly IClaimsService _claimsService;

		public CustomerService(IClaimsService claimsService, IUnitOfWork unitOfWork, IMapper mapper, IMessageBusClient messageBusClient)
		{
			_claimsService = claimsService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_messageBusClient = messageBusClient;
		}
		public async Task<CustomerViewModel> CreateCustomer(CustomerCreateModel model)
		{
			var customer = _mapper.Map<Customer>(model);
			await _unitOfWork.CustomerRepository.AddAsync(customer);
			return await _unitOfWork.SaveChangesAsync() ?
				_mapper.Map<CustomerViewModel>(await _unitOfWork.CustomerRepository.GetByIdAsync(customer.Id))
				: throw new Exception("Save changes failed!");

		}

		public async Task<bool> DeleteCustomer(Guid id)
		{
			var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
			if (customer is not null)
			{
				_unitOfWork.CustomerRepository.SoftRemove(customer);
				return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");

			}
			else throw new Exception($"Not found customer with Id: {id}");
		}

		public async Task<CustomerViewModel> GetCustomerByIdAsync()
		{
			var externalId = _claimsService.GetCurrentUser == Guid.Empty ? throw new Exception($"--> Error: Could not get UserId {_claimsService.GetCurrentUser}") : _claimsService.GetCurrentUser;
			var user = await _unitOfWork.CustomerRepository.FindByField(x => x.ExternalId == externalId);
			if (user is not null)
			{
				return _mapper.Map<CustomerViewModel>(user);
			}
			else throw new Exception($"--> Error: Could not found Customer with ExternalId: {externalId}");
		}

		public async Task<CustomerViewModel> GetCustomerByIdAsync(Guid id)
		{
			var user = await _unitOfWork.CustomerRepository.FindByField(x => x.Id == id);
			if (user is not null)
			{
				return _mapper.Map<CustomerViewModel>(user);
			}
			else throw new Exception($"--> Error: Could not found Customer with Id: {id}");
		}



		public async Task<IEnumerable<CustomerViewModel>> GetCustomers(string search = "")
		{
			return string.IsNullOrEmpty(search) ? _mapper.Map<IEnumerable<CustomerViewModel>>(await _unitOfWork.CustomerRepository.GetAllAsync())
				: _mapper.Map<IEnumerable<CustomerViewModel>>(await _unitOfWork.CustomerRepository.FindListByField(x => x.Email.ToLower().Contains(search.ToLower())));
		}

		public async Task<CustomerViewModel> UpdateCustomer(CustomerUpdateModel model, Guid customerId)
		{
			var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
			if (customer is not null)
			{
				_mapper.Map(model, customer);
				_unitOfWork.CustomerRepository.Update(customer);
				if (await _unitOfWork.SaveChangesAsync())
				{
					_messageBusClient.PublishUpdateAccount(new UserPublishedModel
					{
						Event = nameof(EventType.UserModified),
						Email = customer.Email,
						Name = customer.Name,
						Id = customer.ExternalId,
						PhoneNumber = customer.PhoneNumber,
						Role = nameof(RoleEnum.CUSTOMER)
					});
					return _mapper.Map<CustomerViewModel>(customer);
				}
				else throw new Exception($"--> Error: Save Changes Failed!");
			}
			throw new Exception($"Not found Customer with Id: {customerId}");
		}
	}
}

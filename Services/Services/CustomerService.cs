using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.CustomerModels;

namespace Services.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IClaimsService _claimsService;

		public CustomerService(IClaimsService claimsService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_claimsService = claimsService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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
			if(customer is not null)
			{
				_unitOfWork.CustomerRepository.SoftRemove(customer);
				return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");
				
			} else throw new Exception($"Not found customer with Id: {id}");
        }

        public async Task<CustomerViewModel> GetCustomerById(Guid id)
        => _mapper.Map<CustomerViewModel>(await _unitOfWork.CustomerRepository.GetByIdAsync(id));
            
        

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers(string search = "")
        {
            return string.IsNullOrEmpty(search) ? _mapper.Map<IEnumerable<CustomerViewModel>>(await _unitOfWork.CustomerRepository.GetAllAsync())
				: _mapper.Map<IEnumerable<CustomerViewModel>>(await _unitOfWork.CustomerRepository.FindListByField(x => x.Email.ToLower().Contains(search.ToLower())));
        }

        public async Task<CustomerViewModel> UpdateCustomer(CustomerUpdateModel model)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(model.Id);
			if(customer is not null) 
			{
				_mapper.Map(model, customer);
				_unitOfWork.CustomerRepository.Update(customer);
				return await _unitOfWork.SaveChangesAsync() ? _mapper.Map <CustomerViewModel>(await _unitOfWork.CustomerRepository.GetByIdAsync(customer.Id))
				: throw new Exception("Save changes failed!");
			} throw new Exception($"Not found Customer with Id: {model.Id}");
        }
    }
}

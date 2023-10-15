using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.AsyncDataServices.Interfaces;
using Services.Services.Interfaces;
using Services.ViewModels.AsyncDataModels;
using Services.ViewModels.ProviderModels;

namespace Services.Services
{
	public class ProviderService : IProviderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IClaimsService _claimsService;
		private readonly IMapper _mapper;
		private readonly IMessageBusClient _messsageBusClient;
		public ProviderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IMessageBusClient messageBusClient)
		{
			_mapper = mapper;
			_claimsService = claimsService;
			_unitOfWork = unitOfWork;
			_messsageBusClient = messageBusClient;
		}
		public async Task<ProviderViewModel> CreateAsync(ProviderCreateModel model)
		{
			var provider = _mapper.Map<Provider>(model);
			if (provider is not null)
			{
				await _unitOfWork.ProviderRepository.AddAsync(provider);
				return await _unitOfWork.SaveChangesAsync()
					? _mapper.Map<ProviderViewModel>(await _unitOfWork.ProviderRepository.GetByIdAsync(provider.Id))
					: throw new Exception("Save changes failed!");
			}
			else throw new Exception("Unsupported mapping!");
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var provider = await _unitOfWork.ProviderRepository.GetByIdAsync(id, x => x.Vehicles, x => x.Routes);
			if (provider is not null)
			{
				provider.Status = nameof(StatusEnum.InActive);
				_unitOfWork.ProviderRepository.Update(provider);
				_unitOfWork.ProviderRepository.SoftRemove(provider);
				// Todo, Publish Message, Delete Account of Provider
				return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");
			}
			else throw new Exception($"Not found provider with Id:{id}");
		}

		public async Task<IEnumerable<ProviderViewModel>> GetAllAsync(string search)
		=> _mapper.Map<IEnumerable<ProviderViewModel>>
			(string.IsNullOrEmpty(search) 
			? _mapper.Map<IEnumerable<ProviderViewModel>>(await _unitOfWork.ProviderRepository.GetAllAsync())
			: _mapper.Map<IEnumerable<ProviderViewModel>>(await _unitOfWork.ProviderRepository.FindListByField(x => x.Name.ToLower().Contains(search.ToLower()))));

       
        public async Task<ProviderViewModel> GetByIdAsync()
		{
			var externalId = _claimsService.GetCurrentUser == Guid.Empty ? throw new Exception($"--> Error: ExternalId: {_claimsService.GetCurrentUser}") 
				: _claimsService.GetCurrentUser;
			System.Console.WriteLine($"--> Info: ExternalId: {externalId}");
			var provider = await _unitOfWork.ProviderRepository.FindByField(x => x.ExternalId == externalId) ?? throw new Exception($"--> Error: Not found Provider with LoginId: {_claimsService.GetCurrentUser}"); 
			return _mapper.Map<ProviderViewModel>(provider);
		}
		 public async Task<ProviderViewModel> GetByIdAsync(Guid id)
		{
			
			var provider = await _unitOfWork.ProviderRepository.FindByField(x => x.Id == id) ?? throw new Exception($"--> Error: Not found Provider with Id: {id}"); 
			return _mapper.Map<ProviderViewModel>(provider);
		}
		public async Task<bool> UpdateAsync(ProviderUpdateModel model)
		{
			var provider = await _unitOfWork.ProviderRepository.GetByIdAsync(model.Id!.Value);
			if (provider is not null)
			{
				_mapper.Map(model, provider);
				_unitOfWork.ProviderRepository.Update(provider);
				if(await _unitOfWork.SaveChangesAsync())
				{
					_messsageBusClient.PublishUpdateAccount(new UserPublishedModel
					{
						Id = provider.ExternalId,
						PhoneNumber = provider.PhoneNumber,
						Role = nameof(RoleEnum.PROVIDER),
						Event = nameof(EventType.UserModified),
						Name = provider.Name
						
					});
					return true;
				}else
				throw new Exception("Save changes failed!");
			}
			else
			{
				throw new Exception("Not found!");
			}
		}
	}
}

using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.ProviderModels;

namespace Services.Services
{
	public class ProviderService : IProviderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
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
				
					_unitOfWork.ProviderRepository.SoftRemove(provider);
					return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");

				
			}
			else throw new Exception($"Not found provider with Id:{id}");
		}

		public async Task<IEnumerable<ProviderViewModel>> GetAllAsync()
		=> _mapper.Map<IEnumerable<ProviderViewModel>>
			(await _unitOfWork.ProviderRepository
			.GetAllAsync());




		public async Task<ProviderViewModel> GetByIdAsync(Guid id)
		{
			var provider = await _unitOfWork.ProviderRepository.GetByIdAsync(id, x => x.Routes, x => x.Vehicles);
			return provider is not null
				? _mapper.Map<ProviderViewModel>(provider)
				: throw new Exception("Not found!");



		}

		public async Task<bool> UpdateAsync(ProviderUpdateModel model)
		{
			var provider = await _unitOfWork.ProviderRepository.GetByIdAsync(model.Id);
			if (provider is not null)
			{
				_mapper.Map(model, provider);
				_unitOfWork.ProviderRepository.Update(provider);
				return await _unitOfWork.SaveChangesAsync()
					? true : throw new Exception("Save changes failed!");
			}
			else
			{
				throw new Exception("Not found!");
			}
		}
	}
}

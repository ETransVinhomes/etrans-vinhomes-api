using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.LocationType;

namespace Services.Services
{
	public class LocationTypeService : ILocationTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public LocationTypeService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<LocationTypeViewModel> CreateLocationTypeAsync(LocationTypeCreateModel model)
		{
			var locationType = _mapper.Map<LocationType>(model);
			if (locationType == null) throw new AutoMapperMappingException("Unsupported mapping type");
			await _unitOfWork.LocationTypeRepository.AddAsync(locationType);
			return await _unitOfWork.SaveChangesAsync()
				? _mapper.Map<LocationTypeViewModel>(await _unitOfWork.LocationTypeRepository.GetByIdAsync(locationType.Id))
				: throw new Exception("Save changes failed!");

		}

		public async Task<bool> DeleteLocationTypeAsync(Guid id)
		{
			var locationType = await _unitOfWork.LocationTypeRepository.GetByIdAsync(id, x => x.Locations);
			if (locationType is not null)
			{
				if (locationType.Locations.Count > 0) throw new InvalidDataException("Can not delete! Location type already had location(s)");
				else
				{
					_unitOfWork.LocationTypeRepository.SoftRemove(locationType);
					return await _unitOfWork.SaveChangesAsync()
						? true : throw new Exception("Save changes failed!");
				}
			}
			else throw new InvalidDataException("Not found location type");

		}
		public async Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypeAsync() =>
			_mapper.Map<IEnumerable<LocationTypeViewModel>>(await _unitOfWork.LocationTypeRepository.GetAllAsync());


		public async Task<LocationTypeViewModel> GetLocationTypeByIdAsync(Guid id)
			=> _mapper.Map<LocationTypeViewModel>(await _unitOfWork.LocationTypeRepository.GetByIdAsync(id));

		public async Task<LocationTypeViewModel> UpdateLocationTypeAsync(LocationTypeViewModel model)
		{
			var locationTypeInDb = await _unitOfWork.LocationTypeRepository.GetByIdAsync(model.Id);
			if (locationTypeInDb is null) throw new Exception($"Not found location type with Id: {model.Id}!");
			else
			{
				_mapper.Map(model, locationTypeInDb);
				_unitOfWork.LocationTypeRepository.Update(locationTypeInDb);
				return await _unitOfWork.SaveChangesAsync()
					? _mapper.Map<LocationTypeViewModel>(await _unitOfWork.LocationTypeRepository.GetByIdAsync(locationTypeInDb.Id))
					: throw new Exception("Save changes failed!");
			}
		}
	}
}

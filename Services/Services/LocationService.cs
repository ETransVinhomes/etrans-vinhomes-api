﻿using System.Runtime.CompilerServices;
using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.LocationModels;

namespace Services.Services
{
	public class LocationService : ILocationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

        public async Task<bool> CreateRangeAsync(List<LocationCreateModel> model)
        {
            var locationList = _mapper.Map<List<Location>>(model) ?? throw new Exception("Unsupported Mapping");
			await _unitOfWork.LocationRepository.AddRangeAsync(locationList);
			return await _unitOfWork.SaveChangesAsync() ? true : false;
			

        }

        public async Task<LocationViewModel> CreateAsync(LocationCreateModel model)
		{

			var location = _mapper.Map<Location>(model);
			await _unitOfWork.LocationRepository.AddAsync(location);
			return await _unitOfWork.SaveChangesAsync()
				? _mapper.Map<LocationViewModel>(await _unitOfWork.LocationRepository.GetByIdAsync(location.Id, x => x.LocationType))
				: throw new Exception("Save change failed!");
		}
		
		public async Task<bool> DeleteAsync(Guid id)
		{
			var location = await _unitOfWork.LocationRepository.GetByIdAsync(id, x => x.RouteLocations);
			if (location is not null)
			{
				_unitOfWork.LocationRepository.SoftRemove(location);
				return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");

			}
			else throw new Exception("Not found!");


		}

		public async Task<IEnumerable<LocationViewModel>> FindAsync(string searchString, bool isLocationType = true)
		{
			return isLocationType ?
				_mapper.Map<IEnumerable<LocationViewModel>>(await _unitOfWork.LocationRepository
				.FindListByField(x => x.LocationType.Name.ToLower().Contains(searchString.ToLower()), x => x.LocationType))
				: _mapper.Map<IEnumerable<LocationViewModel>>(await _unitOfWork.LocationRepository
				.FindListByField(x => x.Name.ToLower().Contains(searchString), x => x.LocationType));
		}

		public async Task<IEnumerable<LocationViewModel>> GetAllAsync()
		{
			var locations = await _unitOfWork.LocationRepository.GetAllAsync(x => x.LocationType);
			if (locations.Count > 0)
			{
				return _mapper.Map<IEnumerable<LocationViewModel>>(locations);
			}
			throw new Exception("Location list is emptied! Please add location first");
		}

		public async Task<LocationViewModel> GetByIdAsync(Guid id)
		{
			var location = await _unitOfWork.LocationRepository.GetByIdAsync(id, x => x.LocationType);
			if (location is not null)
			{
				return _mapper.Map<LocationViewModel>(location);
			}
			throw new Exception($"Not found location with Id: {id}");
		}

		public async Task<bool> UpdateAsync(LocationUpdateModel model)
		{
			var location = await _unitOfWork.LocationRepository.GetByIdAsync(model.Id);
			if (location is not null)
			{
				_mapper.Map(model, location);
				_unitOfWork.LocationRepository.Update(location);
				return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");
			}
			else throw new Exception($"Not found location with Id: {model.Id}");
		}
	}
}

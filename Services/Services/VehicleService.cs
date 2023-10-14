using AutoMapper;
using Domain.Entities;
using Hangfire.States;
using Microsoft.Extensions.Logging;
using Services.Services.Interfaces;
using Services.ViewModels.VehicleModels;

namespace Services.Services;
public class VehicleService : IVehicleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    public VehicleService(IMapper mapper, IUnitOfWork unitOfWork, IClaimsService claimsService)
    {
        _claimsService = claimsService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<VehicleViewModel> CreateVehicle(VehicleCreateModel createModel)
    {
        var vehicle = _mapper.Map<Vehicle>(createModel);
        // vehicle.ProviderId = _claimsService.GetCurrentUser;  Should do in this way
        var userId = _claimsService.GetCurrentUser;
        if (userId == Guid.Empty) throw new Exception($"--> Error: Can not get User | UserLoginId: {_claimsService.GetCurrentUser}");
        var provider = await _unitOfWork.ProviderRepository.FindByField(x => x.ExternalId == userId);
        if (provider == null) throw new Exception($"This Provider is not active or not found");


        vehicle.ProviderId = provider.Id;
        if (vehicle.DriverId is not null)
        {
            var driver = (await _unitOfWork.DriverRepository.GetAllAsync()).FirstOrDefault(x => x.Id == vehicle.DriverId);
            if(driver is not null)
            {
                if(driver.Vehicles is not null)
                {
                    if(driver.Vehicles.Any())
                    throw new Exception($"err: Driver with Id: {driver.Id} is take responsible for another Vehicle! Can not Create Vehicle!");
                }
            } else throw new Exception($"err: Driver is null! Can not Create Vehicle!");

        }

        await _unitOfWork.VehicleRepository.AddAsync(vehicle);
        return await _unitOfWork.SaveChangesAsync() ?
            _mapper.Map<VehicleViewModel>(await _unitOfWork.DriverRepository.GetByIdAsync(vehicle.Id))
            : throw new Exception("Save Changes Failed!");

    }

    public async Task<bool> DeleteVehicle(Guid id)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
        if (vehicle is not null)
        {
            _unitOfWork.VehicleRepository.SoftRemove(vehicle);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return true;
            }
            else throw new Exception("Save Changes Failed!");
        }
        else throw new Exception($"Not found Vehicle with Id: {id}");
    }

    public async Task<VehicleViewModel> GetVehicleById(Guid id, bool isProviderInclude = false, bool isDriverInclude = false)
    {
        return isProviderInclude ? isDriverInclude ?
            _mapper.Map<VehicleViewModel>(await _unitOfWork.VehicleRepository.GetByIdAsync(id, x => x.Driver, x => x.Provider))
            : _mapper.Map<VehicleViewModel>(await _unitOfWork.VehicleRepository.GetByIdAsync(id, x => x.Provider))
            : _mapper.Map<VehicleViewModel>(await _unitOfWork.VehicleRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<VehicleViewModel>> GetVehicles(string search = "", bool isDriverInclude = false, bool isProviderInclude = false)
        => !string.IsNullOrEmpty(search)
            ? isDriverInclude ? isProviderInclude ?
            _mapper.Map<IEnumerable<VehicleViewModel>>(await _unitOfWork.VehicleRepository.FindListByField(x => x.Name.ToLower().Contains(search.ToLower()), x => x.Driver, x => x.Provider))
            : _mapper.Map<IEnumerable<VehicleViewModel>>(await _unitOfWork.VehicleRepository.FindListByField(x => x.Name.ToLower().Contains(search.ToLower()), x => x.Driver))
            : _mapper.Map<IEnumerable<VehicleViewModel>>(await _unitOfWork.VehicleRepository.FindListByField(x => x.Name.ToLower().Contains(search.ToLower())))
            : _mapper.Map<IEnumerable<VehicleViewModel>>(await _unitOfWork.VehicleRepository.GetAllAsync());




    public async Task<VehicleViewModel> UpdateVehicle(VehicleUpdateModel updateModel)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(updateModel.Id);
        if (vehicle is not null)
        {
            _mapper.Map(updateModel, vehicle);
            _unitOfWork.VehicleRepository.Update(vehicle);
            return await _unitOfWork.SaveChangesAsync() ?
                _mapper.Map<VehicleViewModel>(await _unitOfWork.VehicleRepository.GetByIdAsync(vehicle.Id))
                : throw new Exception("Save changes failed!");
        }
        else throw new Exception($"Not found Vehicle with Id: {updateModel.Id}");
    }
}
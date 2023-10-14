using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.Services.Interfaces;
using Services.ViewModels.TripModels;
using Services.ViewModels.TripModes;

namespace Services.Services;
public class TripService : ITripService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public TripService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CheckTripStarted()
    {
        (await _unitOfWork.TripRepository.GetAllAsync()).ForEach(x => 
        {
            if(DateTime.Now >= x.StartedDate)
            {
                x.Status = nameof(TransportationStatusEnum.OnGoing);
                _unitOfWork.TripRepository.Update(x);
            }
            
        });
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<TripViewModel> CreateAsync(TripCreateModel model)
    {
        if (string.IsNullOrEmpty(model.Name))
        {
            model.Name = $"Trip|{DateTime.Now.Date}";
        }
        var trip = _mapper.Map<Trip>(model);
        // Check Bussiness Rules

        // Find Route and Get TotalDistance
        var route = await _unitOfWork.RouteRepository.GetByIdAsync(model.RouteId);
        trip.Distance = route != null ? route.TotalDistance : throw new Exception($"Invalid RouteId : {model.RouteId}");

        // Find vehicle and check whether it is actived or not
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(model.VehicleId, x => x.Driver);
        trip.SeatRemain = vehicle != null ? vehicle.TotalSeat - 1 : throw new Exception($"Invalid VehicleId: {model.VehicleId}! Can not create Trip");

        if (vehicle.Status != nameof(TransportationStatusEnum.Active)) throw new Exception($"Vehicle is not active! Can not create trip");
        if (vehicle.Driver == null) throw new Exception($"Trip Create On Vehicle not have any Driver: VehicleId: {vehicle.Id}");
        // Update Vehicle And Driver Status
        vehicle.Status = nameof(TransportationStatusEnum.OnGoing);
        vehicle.Driver.Status = nameof(TransportationStatusEnum.OnGoing);

        _unitOfWork.VehicleRepository.Update(vehicle);
        _unitOfWork.DriverRepository.Update(vehicle.Driver);
        await _unitOfWork.TripRepository.AddAsync(trip);
        if (await _unitOfWork.SaveChangesAsync())
        {
            return _mapper.Map<TripViewModel>(await _unitOfWork.TripRepository.GetByIdAsync(trip.Id));
        }
        else throw new Exception("Save Changes Failed!");

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var trip = await _unitOfWork.TripRepository.GetByIdAsync(id, x => x.Tickets, x => x.Vehicle) ?? throw new Exception($"Not found Trip with Id: {id}");
        if (trip!.Tickets.Count() > 0)
        {
            // Return Back Ticket 
        }
        _unitOfWork.TripRepository.SoftRemove(trip);

        // Update Vehicle Back
        trip.Vehicle.Status = nameof(TransportationStatusEnum.Active);

        _unitOfWork.VehicleRepository.Update(trip.Vehicle);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TripViewModel>> GetAllAsync()
        => _mapper.Map<IEnumerable<TripViewModel>>(await _unitOfWork.TripRepository.GetAllAsync());



    public async Task<TripViewModel> GetByIdAsync(Guid id)
    {
        return _mapper.Map<TripViewModel>(await _unitOfWork.TripRepository.GetByIdAsync(id, x => x.Route, x => x.Vehicle));
    }

    public async Task<TripViewModel> UpdateAsync(TripUpdateModel model)
    {
        var trip = await _unitOfWork.TripRepository.GetByIdAsync(model.Id) ??
        throw new Exception($"Not found Trip with Id: {model.Id}");

        _mapper.Map(model, trip);
        _unitOfWork.TripRepository.Update(trip);
        return await _unitOfWork.SaveChangesAsync()
            ? _mapper.Map<TripViewModel>(await _unitOfWork.TripRepository.GetByIdAsync(model.Id, x => x.Route, x => x.Vehicle))
            : throw new Exception("Save Changes Failed!");
    }
}
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using Services.Services.Interfaces;
using Services.ViewModels.RatingModels;
using Services.ViewModels.TripModels;
using Services.ViewModels.TripModes;

namespace Services.Services;
public class TripService : ITripService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    private readonly ILogger<TripService> _logger;
    public TripService(IMapper mapper, IUnitOfWork unitOfWork, IClaimsService claimsService, ILogger<TripService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _claimsService = claimsService;
    }

    public async Task CheckTripStarted()
    {
        (await _unitOfWork.TripRepository.GetAllAsync()).ForEach(async x => 
        {
            if(x.SeatRemain == 0 && x.Status == nameof(StatusEnum.Active))
            {
                x.Status = nameof(TripStatusEnum.Full);
                _unitOfWork.TripRepository.Update(x);
            }
            if(DateTime.Now.AddHours(7) >= x.StartedDate && (x.Status == nameof(TripStatusEnum.Active) || x.Status == nameof(TripStatusEnum.Full)))
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
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(model.VehicleId, x => x.Driver, x => x.Trips);
        trip.SeatRemain = vehicle != null ? vehicle.TotalSeat - 1 : throw new Exception($"Invalid VehicleId: {model.VehicleId}! Can not create Trip");

        if (vehicle.Status != nameof(TransportationStatusEnum.Active)) 
        {
            if(vehicle.Trips.OrderBy(x => x.StartedDate).First().StartedDate >= trip.StartedDate)
            {
                throw new Exception($"Vehicle is not available the time you choose to start your trip");
            }
        }
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

    public async Task<bool> FinishTrip(Guid id)
    {
        var driverId = _claimsService.GetCurrentUser == Guid.Empty ? throw new Exception($"--> err: Current user is null")
            : _claimsService.GetCurrentUser;

        var trip = await _unitOfWork.TripRepository.GetByIdAsync(id, l => l.Vehicle) ?? throw new Exception($"--> err: Not found trip with Id: {id}");
        trip.Status = nameof(TripStatusEnum.Finished);
        trip.Vehicle.Status = nameof(TransportationStatusEnum.Active);
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(trip.Vehicle.DriverId!.Value) 
        ?? throw new Exception($"--> err: Not found Driver with Id: {trip.Vehicle.DriverId}"); 

        driver.Status = nameof(TransportationStatusEnum.Active);
        _unitOfWork.TripRepository.Update(trip);
        _unitOfWork.DriverRepository.Update(driver);
        _unitOfWork.VehicleRepository.Update(trip.Vehicle);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TripViewModel>> GetAllAsync()
        => _mapper.Map<IEnumerable<TripViewModel>>(await _unitOfWork.TripRepository.GetAllAsync(x => x.Route, x => x.Vehicle));



    public async Task<TripViewModel> GetByIdAsync(Guid id)
    {
        return _mapper.Map<TripViewModel>(await _unitOfWork.TripRepository.GetByIdAsync(id, x => x.Route, x => x.Vehicle));
    }

    public async Task<bool> RatingAsync(Guid id, RatingCreateModel model)
    {
       var userId = _claimsService.GetCurrentUser == Guid.Empty ? throw new Exception($"Login user is null!") : _claimsService.GetCurrentUser;
       var result = await _unitOfWork.TripRepository.RatingTripAsync(id, userId, model);
       if(result)
       {
            return await _unitOfWork.SaveChangesAsync();
       }
       else return false;

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
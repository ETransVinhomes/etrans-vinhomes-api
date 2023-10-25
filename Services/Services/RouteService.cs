using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.RouteModels;

namespace Services.Services;
public class RouteService : IRouteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimsService _claimsService;
    public RouteService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
    {
        _mapper = mapper;
        _claimsService = claimsService;
        _unitOfWork = unitOfWork;
    }
    public async Task<RouteViewModel> CreateAsync(RouteCreateModel model)
    {
        //if (_claimsService.GetCurrentUser == Guid.Empty) throw new Exception($"UnAuthorized");
        var route = _mapper.Map<Route>(model);
        if (route == null) throw new AutoMapperMappingException();
        else
        {
            var provider = await _unitOfWork.ProviderRepository.FindByField(x => x.ExternalId == _claimsService.GetCurrentUser);

            if (provider == null) throw new Exception($"Not found Provider with current login User");
            System.Console.WriteLine($"Provider: {provider.ExternalId} | {provider.Name}");
            route.ProviderId = provider.Id;
            await _unitOfWork.RouteRepository.AddAsync(route);
            if (await _unitOfWork.SaveChangesAsync())
                return _mapper.Map<RouteViewModel>(await _unitOfWork.RouteRepository.GetByIdAsync(route.Id));
            else throw new Exception("Save changes Failed!");
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {

        var route = await _unitOfWork.RouteRepository.GetByIdAsync(id, x => x.RouteLocations);
        if (route is not null)
        {
            if (route.RouteLocations.Count > 0)
                _unitOfWork.RouteLocationRepository.SoftRemoveRange(route.RouteLocations.ToList());
            _unitOfWork.RouteRepository.SoftRemove(route);
            return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");
        }
        else throw new Exception($"Not found Route with Id: {id}");
    }

    public async Task<IEnumerable<RouteViewModel>> GetAllAsync()
     => _mapper.Map<IEnumerable<RouteViewModel>>(await _unitOfWork.RouteRepository.GetAllAsync(x => x.Provider, r => r.RouteLocations));



    public async Task<RouteViewModel> GetByIdAsync(Guid id)
     => _mapper.Map<RouteViewModel>(await _unitOfWork.RouteRepository.GetByIdAsync(id, x => x.Provider, x => x.RouteLocations));



    public async Task<RouteViewModel> UpdateAsync(RouteUpdateModel model)
    {
        var routeInDb = await _unitOfWork.RouteRepository.GetByIdAsync(model.Id);
        if (routeInDb is not null)
        {
            _mapper.Map(model, routeInDb);
            _unitOfWork.RouteRepository.Update(routeInDb);
            if (await _unitOfWork.SaveChangesAsync())
                return _mapper.Map<RouteViewModel>(await _unitOfWork.RouteRepository.GetByIdAsync(model.Id, x => x.Provider));
            else
                throw new Exception("Save Changes Failed!");
        }
        else throw new Exception($"Not Found Route with Id: {model.Id}");
    }
}
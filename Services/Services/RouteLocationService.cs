using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.RouteLocationModels;

namespace Services.Services;
public class RouteLocationService : IRouteLocationService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public RouteLocationService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<RouteLocationViewModel> CreateAsync(RouteLocationCreateModel model, Guid routeId)
    {
        var routeLocation = _mapper.Map<RouteLocation>(model);
        var route = await _unitOfWork.RouteRepository.GetByIdAsync(routeId);
        routeLocation.Index = (await _unitOfWork.RouteLocationRepository.FindListByField(x => x.RouteId == routeId)).Count();
        if (routeLocation.Index == 0) routeLocation.IsHead = true;
        await _unitOfWork.RouteLocationRepository.AddAsync(routeLocation);
        if (await _unitOfWork.SaveChangesAsync())
            return _mapper.Map<RouteLocationViewModel>
            (await _unitOfWork.RouteLocationRepository
            .GetByIdAsync(routeLocation.Id, x => x.Location));
        else throw new Exception($"Save Changes Failed!");
    }

    public async Task<IEnumerable<RouteLocationViewModel>> CreateRangeAsync(List<RouteLocationCreateModel> models, Guid routeId)
    {
        int index = 0;
        var route = await _unitOfWork.RouteRepository.GetByIdAsync(routeId) ?? throw new Exception($"Not found Route with Id: {routeId}");
        var r_l_arr = _mapper.Map<List<RouteLocation>>(models).ToArray();
        r_l_arr.Select(c => { c.RouteId = routeId; return c; }).ToList();
        var existedRouteLoc = (await _unitOfWork.RouteLocationRepository.FindListByField(x => x.RouteId == routeId)).OrderBy(x => x.Index).ToList() ?? new List<RouteLocation>();
        if (existedRouteLoc.Count() > 0)
        {
            index = existedRouteLoc.Count();
            existedRouteLoc.Last().NextRouteLocationId = r_l_arr.First().Id;
        }
        else
        {
            r_l_arr.First().IsHead = true;
        }


        for (int i = index; i < r_l_arr.Count(); i++)
        {
            
            if(string.IsNullOrEmpty(r_l_arr[i].Name)) r_l_arr[i].Name = $"{route.Name} {i}";
            if (i != r_l_arr.Count() - 1)
                r_l_arr[i].NextRouteLocationId = r_l_arr[i + 1].Id;
            r_l_arr[i].RouteId = routeId;
            
            r_l_arr[i].Index = i;
        }
        
        route!.Size = r_l_arr.Count() + 1;
        _unitOfWork.RouteRepository.Update(route);
        await _unitOfWork.RouteLocationRepository.AddRangeAsync(r_l_arr.ToList());
        return await _unitOfWork.SaveChangesAsync() ?
            _mapper.Map<IEnumerable<RouteLocationViewModel>>((await _unitOfWork.RouteLocationRepository.FindListByField(x => x.RouteId == routeId)).OrderBy(x => x.Index))
            : throw new Exception("Create Failed!");
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var routeLocation = await _unitOfWork.RouteLocationRepository.GetByIdAsync(id, x => x.NextRouteLocation!) ?? throw new Exception($"Not found RouteLocation With Id: {id}");
        var route = await _unitOfWork.RouteRepository.GetByIdAsync(routeLocation.RouteId);
        _unitOfWork.RouteLocationRepository.SoftRemove(routeLocation);
        // Update back route size
        route!.Size--;
        _unitOfWork.RouteRepository.Update(route);
        if (route!.Size == 1)
        {
            return await _unitOfWork.SaveChangesAsync();
        }
        if (routeLocation.IsHead)
    {
            routeLocation.NextRouteLocation!.IsHead = true;
            _unitOfWork.RouteLocationRepository.Update(routeLocation.NextRouteLocation);
        }
        else if (routeLocation.NextRouteLocationId == null)
        {
            var prev = await _unitOfWork.RouteLocationRepository.FindByField(x => x.Index == routeLocation.Index - 1 && x.RouteId == route.Id);
            prev.NextRouteLocationId = null;
            prev.NextRouteLocation = null;
            _unitOfWork.RouteLocationRepository.Update(prev);
        }
        else
        {
            var prev = await _unitOfWork.RouteLocationRepository.FindByField(x => x.Index == routeLocation.Index - 1 && x.RouteId == route.Id);
            var nextRouteLocs = await _unitOfWork.RouteLocationRepository.FindListByField(x => x.RouteId == route.Id && x.Index > routeLocation.Index);
            prev.NextRouteLocationId = nextRouteLocs.First().Id;

            nextRouteLocs.OrderBy(x => x.Index);
            foreach (var r_l in nextRouteLocs)
            {
                r_l.Index--;
            }
            _unitOfWork.RouteLocationRepository.Update(prev);
            _unitOfWork.RouteLocationRepository.UpdateRange(nextRouteLocs);
        }
        // Must Checking And Update Index again for list routeLoc

        // Not Done
        return await _unitOfWork.SaveChangesAsync();


    }

    public async Task<IEnumerable<RouteLocationViewModel>> GetAllAsync()
    => _mapper.Map<IEnumerable<RouteLocationViewModel>>(await _unitOfWork.RouteLocationRepository.GetAllAsync());



    public async Task<RouteLocationViewModel> GetByIdAsync(Guid id)
        => _mapper.Map<RouteLocationViewModel>(await _unitOfWork.RouteLocationRepository.GetByIdAsync(id, x => x.Location, x => x.NextRouteLocation!));

    public async Task<IEnumerable<RouteLocationViewModel>> GetRouteLocByRouteId(Guid routeId)
    {
        var results = await _unitOfWork.RouteLocationRepository.FindListByField(x => x.RouteId == routeId, x => x.Location);
        if(results.Count() > 0)
        {
            return _mapper.Map<IEnumerable<RouteLocationViewModel>>(results.OrderBy(x => x.Index));
        } else throw new Exception($"--> Error: Route Location List is Empty! Please Try Again!");
    }

    public Task<RouteLocationViewModel> UpdateAsync(RouteLocationUpdateModel model)
    {
        throw new NotImplementedException();
    }
}
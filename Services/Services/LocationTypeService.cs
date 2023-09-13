using AutoMapper;
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
        public async Task<IEnumerable<LocationTypeDTO>> GetAllLocationTypeAsync() => _mapper.Map<IEnumerable<LocationTypeDTO>>(await _unitOfWork.LocationTypeRepository.GetAllAsync());


        public async Task<LocationTypeDTO> GetLocationTypeByIdAsync(Guid id)
            => _mapper.Map<LocationTypeDTO>(await _unitOfWork.LocationTypeRepository.GetByIdAsync(id));


    }
}

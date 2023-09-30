using System.Security;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Domain.Entities;
using Services.Services.Interfaces;
using Services.ViewModels.DriverModels;

namespace Services.Services
{
    public class DriverService : IDriverService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public DriverService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
        public async Task<DriverViewModel> CreateDriver(DriverCreateModel model)
        {
            var driver = _mapper.Map<Driver>(model);
			await _unitOfWork.DriverRepository.AddAsync(driver);
			return await _unitOfWork.SaveChangesAsync() ? 
				_mapper.Map<DriverViewModel>(await _unitOfWork.DriverRepository.GetByIdAsync(driver.Id)) 
				: throw new Exception("Save changes failed!");

        }

        public async Task<bool> DeleteDriver(Guid id)
        {
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
			if(driver is not null) 
			{
				_unitOfWork.DriverRepository.SoftRemove(driver);
				return await _unitOfWork.SaveChangesAsync() 
					? true : throw new Exception("Save changes failed!");
			} else 
			throw new Exception("Not found");
        }

        public async Task<IEnumerable<DriverViewModel>> GetAllDrivers(string search = "")
			=> _mapper.Map<IEnumerable<DriverViewModel>>(string.IsNullOrEmpty(search) 
			? await _unitOfWork.DriverRepository.GetAllAsync()
			: await _unitOfWork.DriverRepository
			.FindListByField(x => x.LastName.ToLower().Contains(search.ToLower()) || x.FirstName.ToLower().Contains(search.ToLower())));
        public async Task<DriverViewModel> GetDriverById(Guid id)
        => _mapper.Map<DriverViewModel>(await _unitOfWork.DriverRepository.GetByIdAsync(id)); 
        

        public async Task<DriverViewModel> UpdateDriver(DriverUpdateModel model)
        {
            var driver = _mapper.Map<Driver>(model);
			if(driver is not null) 
			{
				_unitOfWork.DriverRepository.Update(driver);
				return await _unitOfWork.SaveChangesAsync() 
					? _mapper.Map<DriverViewModel>(await _unitOfWork.DriverRepository.GetByIdAsync(driver.Id))
					: throw new Exception("Save Changes Failed!");
			} else throw new Exception("Not found!");
        }
    }
}

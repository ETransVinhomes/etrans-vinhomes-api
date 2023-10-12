using System.Security;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Services.AsyncDataServices.Interfaces;
using Services.Services.Interfaces;
using Services.ViewModels.DriverModels;

namespace Services.Services
{
    public class DriverService : IDriverService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IClaimsService _claimsService;
		private readonly IMessageBusClient _messageBusClient;
		public DriverService(IMapper mapper, IUnitOfWork unitOfWork, IClaimsService claimsService, IMessageBusClient messageBusClient)
		{
			_mapper = mapper;
			_claimsService = claimsService;
			_unitOfWork = unitOfWork;
			_messageBusClient = messageBusClient;
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
			.FindListByField(x => x.Name.ToLower().Contains(search.ToLower())));
        public async Task<DriverViewModel> GetDriverById()
		{
			var externalId = _claimsService.GetCurrentUser == Guid.Empty ? throw new Exception($"--> Error: ExternalId is null") : _claimsService.GetCurrentUser;
			System.Console.WriteLine($"--> Info: ExternalId: {externalId}");
			var driver = await _unitOfWork.DriverRepository.FindByField(x => x.ExternalId == externalId) ?? throw new Exception($"--> Error: Not found Driver with externalId: {externalId}");
			return _mapper.Map<DriverViewModel>(driver);
		}
        
        

        public async Task<DriverViewModel> UpdateDriver(DriverUpdateModel model)
        {
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(model.Id);
			if(driver is not null) 
			{
				_mapper.Map(model, driver);
				_unitOfWork.DriverRepository.Update(driver);
				if(await _unitOfWork.SaveChangesAsync())
				{
					_messageBusClient.PublishUpdateAccount(new ViewModels.AsyncDataModels.UserPublishedModel
					{
						Id = driver.ExternalId,
						Email = "",
						Event = nameof(EventType.UserModified),
						PhoneNumber = driver.PhoneNumber,
						Name =driver.Name,
						Role = nameof(RoleEnum.DRIVER)
					});
					return _mapper.Map<DriverViewModel>(await _unitOfWork.DriverRepository.GetByIdAsync(driver.Id));
				} else throw new Exception($"--> Error: Save Changes Failed!");
			} else throw new Exception("Not found!");
        }
    }
}

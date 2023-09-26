using AutoMapper;
using Domain.Entities;
using Services.ViewModels.LocationModels;
using Services.ViewModels.LocationType;
using Services.ViewModels.ProviderModels;

namespace Repositories.Mappers
{
	public class MapperConfigurationProfiles : Profile
	{
		public MapperConfigurationProfiles()
		{
			#region LocationTypeMapping
			CreateMap<LocationType, LocationTypeViewModel>().ReverseMap();
			CreateMap<LocationType, LocationTypeCreateModel>().ReverseMap();
			#endregion
			#region Location Mapping
			CreateMap<Location, LocationCreateModel>().ReverseMap();
			CreateMap<Location, LocationViewModel>().ReverseMap();
			CreateMap<Location, LocationUpdateModel>().ReverseMap();

			#endregion
			#region ProviderMapping
			CreateMap<Provider, ProviderCreateModel>().ReverseMap();
			CreateMap<Provider, ProviderUpdateModel>().ReverseMap();
			CreateMap<Provider, ProviderViewModel>().ReverseMap();
			#endregion
		}
	}
}

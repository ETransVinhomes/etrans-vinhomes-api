using AutoMapper;
using Domain.Entities;
using Services.ViewModels.LocationType;

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


			#endregion
		}
	}
}

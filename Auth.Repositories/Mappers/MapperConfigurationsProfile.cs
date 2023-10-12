using Auth.Domains.Entities;
using Auth.Services.ViewModels;
using AutoMapper;

namespace Auth.Repositories.Mappers
{
	public class MapperConfigurationsProfile : Profile
	{
		public MapperConfigurationsProfile()
		{
		CreateMap<UserViewModel, AppUser>().ReverseMap();
		}
	}
}

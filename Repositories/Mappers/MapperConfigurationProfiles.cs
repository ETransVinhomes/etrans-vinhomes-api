using AutoMapper;
using Domain.Entities;
using Services.ViewModels.CustomerModels;
using Services.ViewModels.DriverModels;
using Services.ViewModels.LocationModels;
using Services.ViewModels.LocationType;
using Services.ViewModels.OrderModels;
using Services.ViewModels.PaymentModels;
using Services.ViewModels.ProviderModels;
using Services.ViewModels.RouteLocationModels;
using Services.ViewModels.RouteModels;
using Services.ViewModels.TicketModels;
using Services.ViewModels.TripModels;
using Services.ViewModels.TripModes;
using Services.ViewModels.VehicleModels;

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

			#region  DriverMapping
			CreateMap<Driver, DriverUpdateModel>().ReverseMap();
			CreateMap<DriverCreateModel, Driver>().ReverseMap();
			CreateMap<DriverViewModel, Driver>().ReverseMap();
			#endregion

			#region  VehicleMapping
			CreateMap<VehicleViewModel, Vehicle>().ReverseMap();
			CreateMap<VehicleUpdateModel, Vehicle>().ReverseMap();
			CreateMap<VehicleCreateModel, Vehicle>().ReverseMap();
			#endregion

			#region  UserMapping
			CreateMap<Customer, CustomerCreateModel>().ReverseMap();
			CreateMap<Customer, CustomerViewModel>().ReverseMap();
			CreateMap<Customer, CustomerUpdateModel>().ReverseMap();
			#endregion

			#region  RouteMapping
			CreateMap<RouteCreateModel, Route>().ReverseMap();
			CreateMap<RouteViewModel, Route>().ReverseMap();
			CreateMap<RouteUpdateModel, Route>().ReverseMap();
			#endregion
			

			#region  RouteLocationMapping
			CreateMap<RouteLocationViewModel, RouteLocation>().ReverseMap();
			CreateMap<RouteLocationCreateModel, RouteLocation>().ReverseMap();
			CreateMap<RouteLocationUpdateModel, RouteLocationUpdateModel>().ReverseMap();
			#endregion

			#region  TripMapping
			CreateMap<Trip, TripViewModel>().ReverseMap();
			CreateMap<TripCreateModel, Trip>().ReverseMap();
			CreateMap<TripUpdateModel, Trip>().ReverseMap();
			#endregion

			#region OrderMapping
			CreateMap<OrderCreateModel, Order>().ReverseMap();
			CreateMap<OrderViewModel, Order>().ReverseMap();
			CreateMap<OrderUpdateModel, Order>().ReverseMap();
			#endregion  

			#region TicketMapping
			CreateMap<TicketCreateModel, Ticket>().ReverseMap();
			CreateMap<TicketViewModel, Ticket>().ReverseMap();
			CreateMap<TicketUpdateModel, Ticket>().ReverseMap();
			#endregion

			#region PaymentMapping
			CreateMap<PaymentViewModel, Payment>().ReverseMap();
			CreateMap<PaymentCreateModel, Payment>().ReverseMap();
			#endregion
			
		}
	}
}

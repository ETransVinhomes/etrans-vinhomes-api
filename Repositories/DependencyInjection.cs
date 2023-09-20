using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Mappers;
using Repositories.Repositories;
using Services;
using Services.Repositories;
using Services.Services;
using Services.Services.Interfaces;

namespace Repositories
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services, string dbConnection)
		{

			services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
			services.AddScoped<ICurrentTime, CurrentTime>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(typeof(MapperConfigurationProfiles).Assembly);
			#region DI-REPOSITORIES
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IDriverRepository, DriverRepository>();
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<ILocationTypeRepository, LocationTypeRepository>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddScoped<IProviderRepository, ProviderRepository>();
			services.AddScoped<IRouteLocationRepository, RouteLocationRepository>();
			services.AddScoped<IRouteRepository, RouteRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<ITicketRepository, TickeRepository>();
			services.AddScoped<ITripRepository, TripRepository>();
			services.AddScoped<IVehicleRepository, VehicleRepository>();
			#endregion

			#region DI_SERVICES
			services.AddScoped<ILocationTypeService, LocationTypeService>()
				.AddScoped<ILocationService, LocationService>();
			#endregion
			return services;
		}
	}
}

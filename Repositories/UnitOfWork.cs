using Services;
using Services.Repositories;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationTypeRepository _locationTypeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IRouteLocationRepository _routeLocationRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public UnitOfWork(AppDbContext appDbContext, ICustomerRepository customerRepository, IDriverRepository driverRepository
                        , ILocationRepository locationRepository, ILocationTypeRepository locationTypeRepository
            , IOrderRepository orderRepository, IPaymentRepository paymentRepository, IProviderRepository providerRepository
            , IRouteLocationRepository routeLocationRepository, IRouteRepository routeRepository, ITicketRepository ticketRepository
            , IVehicleRepository vehicleRepository, ITripRepository tripRepository)
        {
            _appDbContext = appDbContext;
            _customerRepository = customerRepository;
            _driverRepository = driverRepository;
            _locationRepository = locationRepository;
            _locationTypeRepository = locationTypeRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _providerRepository = providerRepository;
            _routeLocationRepository = routeLocationRepository;
            _routeRepository = routeRepository;
            _ticketRepository = ticketRepository;
            _tripRepository = tripRepository;
            _vehicleRepository = vehicleRepository;
        }

        public ICustomerRepository CustomerRepository => _customerRepository;

        public IDriverRepository DriverRepository => _driverRepository;

        public ILocationRepository LocationRepository => _locationRepository;

        public ILocationTypeRepository LocationTypeRepository => _locationTypeRepository;

        public IOrderRepository OrderRepository => _orderRepository;

        public IPaymentRepository PaymentRepository => _paymentRepository;

        public IProviderRepository ProviderRepository => _providerRepository;

        public IRouteLocationRepository RouteLocationRepository => _routeLocationRepository;

        public IRouteRepository RouteRepository => _routeRepository;

        public ITicketRepository TicketRepository => _ticketRepository;

        public ITripRepository TripRepository => _tripRepository;

        public IVehicleRepository VehicleRepository => _vehicleRepository;

        public async Task<bool> SaveChangesAsync() => await _appDbContext.SaveChangesAsync() > 0;


    }
}

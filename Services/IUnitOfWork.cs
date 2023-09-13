using Services.Repositories;

namespace Services
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public IDriverRepository DriverRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public ILocationTypeRepository LocationTypeRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IProviderRepository ProviderRepository { get; }
        public IRouteLocationRepository RouteLocationRepository { get; }
        public IRouteRepository RouteRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITripRepository TripRepository { get; }
        public IVehicleRepository VehicleRepository { get; }

        public Task<bool> SaveChangesAsync();
    }
}

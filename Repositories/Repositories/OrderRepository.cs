using Domain.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}

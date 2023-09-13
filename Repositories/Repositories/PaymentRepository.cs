using Domain.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Repositories.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}

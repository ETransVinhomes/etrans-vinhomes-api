using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels.RatingModels;

namespace Repositories.Repositories
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        private readonly AppDbContext _dbContext;
        public TripRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService) : base(dbContext, currentTime, claimsService)
        {
                _dbContext = dbContext;
        }

        public async Task<bool> RatingTripAsync(Guid tripId, Guid userId, RatingCreateModel model)
        {
            var trip = await  _dbContext.Trip.Where(x => x.Id == tripId && x.Status == nameof(TripStatusEnum.Finished))
                            .Include(x => x.Vehicle)
                            .ThenInclude(x => x.Driver)
                            .FirstAsync();
            var isBookedTicket = await _dbContext.Ticket.Where(x => x.TripId == tripId && x.Order.CustomerId == userId).FirstOrDefaultAsync();
            if(isBookedTicket is not null && trip.Vehicle.Driver is not null)
            {
            
                trip.Rating = (model.TripRating + trip.Rating) / 2;
                trip.Vehicle.Driver.Rating = (model.DriverRating + trip.Vehicle.Driver.Rating) / 2;
                _dbContext.Trip.Update(trip);
                _dbContext.Driver.Update(trip.Vehicle.Driver);
                return true;
            } else  
            return false;
        }
    }
}

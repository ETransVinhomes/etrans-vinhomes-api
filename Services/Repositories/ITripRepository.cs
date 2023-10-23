using Domain.Entities;
using Services.ViewModels.RatingModels;

namespace Services.Repositories
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        Task<bool> RatingTripAsync(Guid tripId, Guid userId, RatingCreateModel model);
    }

    
    
}

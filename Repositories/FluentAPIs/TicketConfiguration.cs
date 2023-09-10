using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.FluentAPIs
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Trip)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.TripId);
        }
    }
}

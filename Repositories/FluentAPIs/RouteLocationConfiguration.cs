using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.FluentAPIs
{
    public class RouteLocationConfiguration : IEntityTypeConfiguration<RouteLocation>
    {
        public void Configure(EntityTypeBuilder<RouteLocation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Route).WithMany(x => x.RouteLocations)
                .HasForeignKey(x => x.RouteId);
            builder.HasOne(x => x.Location)
                .WithMany(x => x.RouteLocations)
                .HasForeignKey(x => x.LocationId)
                .HasConstraintName("Location_RouteLocation").OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.NextRouteLocation)
                            .WithMany(x => x.ChildsRouteLocation)
                            .HasForeignKey(x => x.NextRouteLocationId)
                            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

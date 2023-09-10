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
            builder.HasOne(x => x.EndLocation)
                .WithMany(x => x.EndRouteLocations)
                .HasForeignKey(x => x.EndLocationId)
                .HasConstraintName("FK_End_Location_RouteLocation").OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.StartLocation)
                .WithMany(x => x.StartRouteLocations)
                .HasForeignKey(x => x.StartLocationId)
                .HasConstraintName("FK_Start_Location_RouteLocation").OnDelete(DeleteBehavior.NoAction);
        }
    }
}

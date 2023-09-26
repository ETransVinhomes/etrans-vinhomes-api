using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.FluentAPIs
{
	public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
	{
		public void Configure(EntityTypeBuilder<Vehicle> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.Provider)
				.WithMany(x => x.Vehicles)
				.HasForeignKey(x => x.ProviderId);
			builder.HasOne(x => x.Driver)
				.WithMany(x => x.Vehicles)
				.HasForeignKey(x => x.DriverId)
				.IsRequired(false);

		}
	}
}

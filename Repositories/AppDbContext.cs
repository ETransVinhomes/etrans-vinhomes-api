using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<Payment> Payment { get; set; } = default!;
        public DbSet<Ticket> Ticket { get; set; } = default!;
        public DbSet<Trip> Trip { get; set; } = default!;
        public DbSet<Driver> Driver { get; set; } = default!;
        public DbSet<Vehicle> Vehicle { get; set; } = default!;
        public DbSet<LocationType> LocationType { get; set; } = default!;
        public DbSet<Location> Location { get; set; } = default!;
        public DbSet<Route> Route { get; set; } = default!;
        public DbSet<RouteLocation> RouteLocation { get; set; } = default!;
        public DbSet<Provider> Provider { get; set; } = default!;



        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
            // Ignore cycle 
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            
          
            modelBuilder.Entity<LocationType>().HasData(new LocationType
            {
                Name = "Indoor"
            },
            new LocationType
            {
                Name = "Outdoor"
            });
        }


    }
}

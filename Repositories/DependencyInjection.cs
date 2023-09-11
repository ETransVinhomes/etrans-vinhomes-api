using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string dbConnection)
        {

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            services.AddScoped<ICurrentTime, CurrentTime>();
            return services;
        }
    }
}

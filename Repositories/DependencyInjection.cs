using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            return services;
        }
    }
}

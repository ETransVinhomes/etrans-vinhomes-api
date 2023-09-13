using ETransVinhomesAPI.Services;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            services.AddSingleton<IClaimsService, ClaimsService>();
            return services;
        }
    }
}

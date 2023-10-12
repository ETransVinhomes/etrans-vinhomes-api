using Auth.Repositories.Data;
using Auth.Repositories.Mappers;
using Auth.Repositories.Repositories;
using Auth.Services.AsyncDataServices;
using Auth.Services.AsyncDataServices.Interfaces;
using Auth.Services.EventProcessing;
using Auth.Services.EventProcessing.Interface;
using Auth.Services.Repositories;
using Auth.Services.Services;
using Auth.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Repositories
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddRepositoriesServices(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddHostedService<MessageBusSuscriber>();
            return services;
        }
    }
}

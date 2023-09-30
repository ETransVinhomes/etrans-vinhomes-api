using Auth.Domains.Entities;
using Auth.Repositories.Data;
using Auth.Services.Commons;
using ETransVinhomes.AuthAPI.Middlewares;
using Microsoft.AspNetCore.Identity;

namespace ETransVinhomes.AuthAPI
{
	public static class DependencyInjection
	{
		public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<GlobalExceptionMiddleware>();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
			builder.Services.AddIdentity<AppUser, AppRole>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();
			return builder;
		}
	}
}

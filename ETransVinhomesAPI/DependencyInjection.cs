using ETransVinhomesAPI.Middlewares;
using ETransVinhomesAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Services.Interfaces;
using System.Reflection;
using System.Text;

namespace ETransVinhomesAPI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebApiServices(this IServiceCollection services)
		{
			services.AddControllers().AddOData(opt =>
		   {
			   opt.Filter().Select().OrderBy().SetMaxTop(100).Expand();
		   });
			services.AddRouting(opt => opt.LowercaseUrls = true);
			services.AddControllers();
			services.AddTransient<GlobalExceptionMiddleware>();
			services.AddValidatorsFromAssembly(typeof(Program).Assembly);
			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(opt =>
			{
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				opt.IncludeXmlComments(xmlPath);
				opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Bearer Generated JWT-Token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"

				});
				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							}
						}, new string[] { }
					}
				});
			});
			services.AddHealthChecks();

			services.AddHttpContextAccessor();
			services.AddScoped<IClaimsService, ClaimsService>();
			services.AddHangfire(config => config
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseInMemoryStorage());

			services.AddHangfireServer();
			return services;
		}

		public static WebApplicationBuilder AddETransAuthentication(this WebApplicationBuilder builder)
		{
			var settingsSection = builder.Configuration.GetSection("ApiSettings");
			var secret = settingsSection.GetValue<string>("Secret");
			var issuer = settingsSection.GetValue<string>("Issuer");
			var audience = settingsSection.GetValue<string>("Audience");
			var key = Encoding.ASCII.GetBytes(secret!);
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					ValidateAudience = true
				};
			});
			builder.Services.AddAuthentication();
			return builder;
		}
	}
}

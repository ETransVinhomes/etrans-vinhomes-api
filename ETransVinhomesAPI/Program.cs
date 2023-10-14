using ETransVinhomesAPI;
using ETransVinhomesAPI.Middlewares;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Serilog;
using Serilog.Events;
using Services.Services.Interfaces;
Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateBootstrapLogger();
try
{
	var builder = WebApplication.CreateBuilder(args);
	builder.Host.UseSerilog((context, services, configuration) => configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext());
	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddServices(builder.Configuration.GetConnectionString("Default")!);
	builder.Services.AddWebApiServices();
	builder.Services.AddScoped<BackGroundTripService>();
	builder.AddETransAuthentication();
	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}
	else
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}
	ApplyMigration();
	app.UseHttpsRedirection();
	app.UseMiddleware<GlobalExceptionMiddleware>();
	app.UseAuthentication();
	app.UseAuthorization();

	
	app.MapControllers();
	app.UseSerilogRequestLogging(configure =>
	{
		configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
	});
	app.MapHangfireDashboard("/HangfireDashBoard");
	RecurringJob.AddOrUpdate<ITripService>("updated trip", x => x.CheckTripStarted(), Cron.Minutely());
	app.Run();
	void ApplyMigration()
	{
		using (var scope = app!.Services.CreateScope())
		{
			var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			if (_db.Database.GetPendingMigrations().Count() > 0)
			{

				_db.Database.Migrate();
			}
		}
	}
}
catch (Exception ex)
{
	Log.Fatal(ex, "Host Terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}



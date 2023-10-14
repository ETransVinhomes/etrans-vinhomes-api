using Auth.Repositories;
using Auth.Repositories.Data;
using ETransVinhomes.AuthAPI;
using ETransVinhomes.AuthAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
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
	builder.AddApiServices();
	builder.Services.AddRepositoriesServices(builder.Configuration.GetConnectionString("Default")!);
	var app = builder.Build();

	// Configure the HTTP request pipeline.

	app.UseSwagger();
	app.UseSwaggerUI();


	app.UseHttpsRedirection();
	app.UseMiddleware<GlobalExceptionMiddleware>();
	app.UseAuthorization();
	app.UseSerilogRequestLogging(configure =>
	{
		configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
	});
	app.MapControllers();
	ApplyMigration();
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
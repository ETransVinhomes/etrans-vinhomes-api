using Auth.Services.ViewModels.ResponseModels;
using Newtonsoft.Json;
using System.Net;

namespace ETransVinhomes.AuthAPI.Middlewares
{
	public class GlobalExceptionMiddleware : IMiddleware
	{
		private readonly ILogger<GlobalExceptionMiddleware> _logger;
		private readonly RequestDelegate _next;
		public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Response.ContentType = "application/json";
				_logger.LogError(ex.Message);
				var result = JsonConvert.SerializeObject(new ResponseModel
				{
					IsSuccess = false,
					Message = ex.Message
				});
				await context.Response.WriteAsync(result);
			}
		}
	}
}

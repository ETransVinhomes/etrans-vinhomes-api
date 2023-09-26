using Newtonsoft.Json;
using Services.ViewModels.ResponseModels;
using System.Net;

namespace ETransVinhomesAPI.Middlewares
{
	public class GlobalExceptionMiddleware : IMiddleware
	{
		private readonly ILogger<GlobalExceptionMiddleware> _logger;
		public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
		{
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
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

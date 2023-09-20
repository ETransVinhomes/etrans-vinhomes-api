namespace Services.ViewModels.ResponseModels
{
	public class ResponseModel
	{
		public bool IsSuccess { get; set; } = true;
		public object? Result { get; set; } = default!;
		public string Message { get; set; } = "";
	}
}

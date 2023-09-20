using Microsoft.IdentityModel.Tokens;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace ETransVinhomesAPI.Services
{
	public class ClaimsService : IClaimsService
	{
		public ClaimsService(IHttpContextAccessor httpContextAccessor)
		{
			// todo implementation to get the current userId
			var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
			GetCurrentUser = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);

			var email = httpContextAccessor.HttpContext?.User.FindFirstValue("email");
			GetEmail = email.IsNullOrEmpty() ? "" : email!.ToString();

			var phoneNo = httpContextAccessor.HttpContext?.User.FindFirstValue("PhoneNumber");
			GetPhoneNumber = phoneNo.IsNullOrEmpty() ? "" : phoneNo!.ToString();
			var name = httpContextAccessor.HttpContext?.User.FindFirstValue("name");
			GetName = name.IsNullOrEmpty() ? "" : name!.ToString();

		}
		public string GetEmail { get; }

		public Guid GetCurrentUser { get; }

		public string GetName { get; }

		public string GetPhoneNumber { get; }
	}
}

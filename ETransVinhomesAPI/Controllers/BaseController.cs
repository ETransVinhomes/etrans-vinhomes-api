using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ETransVinhomesAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]s")]
	[ApiController]
	public class BaseController : ODataController
	{
	}
}

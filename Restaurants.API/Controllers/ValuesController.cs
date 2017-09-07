using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
    [Route("api/values")]
    public class ValuesController : Controller
    {
		[HttpGet]
		[AllowAnonymous]
		public IActionResult GetValues(){
			string[] values = new string[2];
			values[0] = "Hello";
			values[1] = "World!";
			return Ok(values);
		}
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Models.Api;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurant")]
	public class RestaurantController : BaseController
	{

		[HttpGet("")]
		[AllowAnonymous]
		public async Task<IActionResult> GetRestaurantsByOwnerAsync(long ownerId, int pageNumber = 1, int pageSize = 10)
		{
			List<RestaurantObjects> result;
			try
			{
				result = await Services.GetOwnerRestaurantsAsync(ownerId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAsync(long id)
		{
			RestaurantObjects result;
			try
			{
				result = await Services.GetRestaurantAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddRestaurantAsync([FromBody] RestaurantsModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Tuple<RestaurantObjects, EmployersRestaurants> result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Services.AddNewRestaurantAsync(
					currentUser.ThePersonAsEmployer.Id,
					model.Name, model.Description);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result.ToValueTuple());
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRestaurantAsync(int id, [FromBody] RestaurantsModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			RestaurantObjects result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Services.UpdateRestaurantAsync(
					currentUser.ThePersonAsEmployer.Id,
					id, model.Name, model.Description);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> CloseRestaurantAsync(int id)
		{
			bool result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Services.CloseRestaurantAsync(currentUser.ThePersonAsEmployer.Id, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}
	}
}
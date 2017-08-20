using System;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Models.Api;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurant")]
	public class RestaurantController : BaseController
	{

		[HttpGet("")]
		[AllowAnonymous]
		public IActionResult GetRestaurantsByOwner(long ownerId, int pageNumber = 1, int pageSize = 10)
		{
			List<RestaurantObjects> result;
			try
			{
				result = Services.GetOwnerRestaurants(ownerId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}		

		[HttpGet("{id}")]
		[AllowAnonymous]
		public IActionResult Get(long id)
		{
			RestaurantObjects result;
			try
			{
				result = Services.GetRestaurant(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPost("")]
		public IActionResult AddRestaurant([FromBody] RestaurantsModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Tuple<RestaurantObjects, EmployersRestaurants> result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Services.AddNewRestaurant(
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
		public IActionResult UpdateRestaurant(int id, [FromBody] RestaurantsModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			RestaurantObjects result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Services.UpdateRestaurant(
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
		public IActionResult CloseRestaurant(int id)
		{
			bool result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Services.CloseRestaurant(currentUser.ThePersonAsEmployer.Id, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}
	}
}
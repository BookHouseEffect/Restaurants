using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Models.EntityFramework;
using System.Net;
using Restaurants.API.Models.Api;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurant/owners")]
    public class EmployerRestaurantController : BaseController
    {
		private RestaurantService Service;

		public EmployerRestaurantController() : base()
		{
			Service = new RestaurantService(new Models.Context.AppDbContext(), this.GetCurrentUser());
		}

		[HttpGet("")]
		[AllowAnonymous]
		public IActionResult GetOwnersByRestaurant(long restaurantId, int pageNumber = 1, int pageSize = 10)
		{
			List<EmployersRestaurants> result;
			try
			{
				result = Service.GetRestaurantOwners(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPost("")]
		public IActionResult AddCoowner([FromBody] RestaurantCoownerModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			EmployersRestaurants result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Service.AddCoowner(
					currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId ?? 0, model.EmployerId ?? 0);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPut("{id}")]
		public IActionResult TranserOwnership(int id, [FromBody] CoownerModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Boolean result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Service.TransferOwnership(
					currentUser.ThePersonAsEmployer.Id,
					id, model.EmployerId ?? 0);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public IActionResult RemoveCoowner(int id /*restaurantId*/, int coownerId)
		{
			bool result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = Service.RemoveCoowner(
					currentUser.ThePersonAsEmployer.Id, id, coownerId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}
	}
}
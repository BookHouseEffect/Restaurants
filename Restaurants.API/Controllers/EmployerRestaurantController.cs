using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Models.EntityFramework;
using System.Net;
using Restaurants.API.Models.Api;
using System.Threading.Tasks;

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
		public async Task<IActionResult> GetOwnersByRestaurantAsync(long restaurantId, int pageNumber = 1, int pageSize = 10)
		{
			List<EmployersRestaurants> result;
			try
			{
				result = await Service.GetRestaurantOwnersAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddCoownerAsync([FromBody] RestaurantCoownerModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			EmployersRestaurants result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Service.AddCoownerAsync(
					currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, model.EmployerId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> TranserOwnershipAsync(int id, [FromBody] CoownerModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Boolean result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Service.TransferOwnershipAsync(
					currentUser.ThePersonAsEmployer.Id,
					id, model.EmployerId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveCoownerAsync(int id /*restaurantId*/, int coownerId)
		{
			bool result;
			try
			{
				People currentUser = this.GetCurrentUser();
				result = await Service.RemoveCoownerAsync(
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
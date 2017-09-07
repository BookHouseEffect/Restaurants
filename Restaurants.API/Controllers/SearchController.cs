using Restaurants.API.Models.Api;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Services.Implementation;
using Restaurants.API.Models.EntityFramework;
using System.Net;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
	[Route("api/search")]
	public class SearchController : BaseController
	{
		SearchService Service;

		public SearchController()
		{
			Service = new SearchService(new Models.Context.AppDbContext());
		}

		[HttpGet("owner")]
		[AllowAnonymous]
		public async Task<IActionResult> SearchEmployersAsync(long searchForRestaurantId, string searchTerm = "", int pageNumber = 1, int pageSize = 10)
		{
			List<Employers> result;
			try
			{
				result = await Service.SearchEmployersAsync(searchTerm, searchForRestaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}

		[HttpGet("restaurant")]
		[AllowAnonymous]
		public async Task<IActionResult> SearchRestaurantsAsync(string searchTerm = "", float currentLongitude = 0, float currentLatitude = 0, int pageNumber = 1, int pageSize = 20)
		{
			List<SearchRestaurantResult> result;
			try
			{
				result = await Service.SearchRestaurantAsync(searchTerm, currentLongitude, currentLatitude, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}
	}
}

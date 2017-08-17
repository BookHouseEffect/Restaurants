using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurants.API.Services.Implementation;
using Restaurants.API.Models.EntityFramework;
using System.Net;

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
		public IActionResult SearchEmployers(long searchForRestaurantId, string searchTerm = "",  int pageNumber = 1, int pageSize = 10)
		{
			List<Employers> result;
			try
			{
				result = Service.SearchEmployers(searchTerm, searchForRestaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(result);
		}
	}
}
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Restaurants.API.Models.Api;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
    [Route("api/restaurant/menuLanguages")]
    public class MenuLanguageController : BaseController
    {
		[AllowAnonymous]
		[HttpGet("list")]
		public async Task<IActionResult> GetLanguageListAsync()
		{

			List<Languages> list;
			try
			{
				list = await Services.GetAllAvailableLanguagesAsync();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(list);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleMenuLanguageAsync(long id)
		{
			MenuLanguages existingMenuLanguage;
			try
			{
				existingMenuLanguage = await Services.GetMenuLanguageAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuLanguage);
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<IActionResult> GetMenuLanguagesAsync(long restaurantId, int pageNumber=1, int pageSize = 10)
		{
			List<MenuLanguages> existingMenuLanguage;
			try
			{
				existingMenuLanguage = await Services.GetMenuLanguagesPagedAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuLanguage);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddMenuLanguageAsync([FromBody] LanguageModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuLanguages newMenuLanguage;
			try
			{
				People currentUser = GetCurrentUser();
				newMenuLanguage = await Services.AddMenuLanguageAsync(currentUser.ThePersonAsEmployer.Id, 
						model.RestaurantId, model.LanguageId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newMenuLanguage);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMenuLanguageAsync(long id, [FromBody] LanguageModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuLanguages existingMenuLanguage;
			try
			{
				People currentUser = GetCurrentUser();
				existingMenuLanguage = await Services.UpdateMenuLanguageAsync(currentUser.ThePersonAsEmployer.Id, model.RestaurantId, id, model.LanguageId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuLanguage);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMenuLanguage(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveMenuLanguageAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}

	}
}
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
    [Route("api/restaurant/menuCurrencies")]
    public class MenuCurrencyController : BaseController
    {
		[AllowAnonymous]
		[HttpGet("list")]
		public async Task<IActionResult> GetCurrencyListAsync()
		{

			List<Currencies> list;
			try
			{
				list = await Services.GetAllAvailableCurrenciesAsync();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(list);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleMenuCurrencyAsync(long id)
		{
			MenuCurrencies existingMenuCurrency;
			try
			{
				existingMenuCurrency = await Services.GetMenuCurrencyAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCurrency);
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<IActionResult> GetMenuCurrenciesAsync(long restaurantId, int pageNumber=1, int pageSize = 10)
		{
			List<MenuCurrencies> existingMenuCurrencies;
			try
			{
				existingMenuCurrencies = await Services.GetMenuCurrenciesPagedAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCurrencies);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddMenuCurrencyAsync([FromBody] CurrencyModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuCurrencies newMenuCurrency;
			try
			{
				People currentUser = GetCurrentUser();
				newMenuCurrency = await Services.AddMenuCurrencyAsync(currentUser.ThePersonAsEmployer.Id, 
						model.RestaurantId, model.CurrencyId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newMenuCurrency);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMenuCurrencyAsync(long id, [FromBody] CurrencyModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuCurrencies existingMenuCurrency;
			try
			{
				People currentUser = GetCurrentUser();
				existingMenuCurrency = await Services.UpdateMenuCurrenciesAsync(currentUser.ThePersonAsEmployer.Id, model.RestaurantId, id, model.CurrencyId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCurrency);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMenuCurrency(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveMenuCurrencyAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}

	}
}
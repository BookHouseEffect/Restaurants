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
    [Route("api/restaurant/menuItems")]
    public class MenuItemController : BaseController
    {

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleMenuItemAsync(long id)
		{
			MenuItems existingMenuItem;
			try
			{
				existingMenuItem = await Services.GetMenuItemAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuItem);
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<IActionResult> GetMenuItemsAsync(long restaurantId, long menuCategoryId, int pageNumber=1, int pageSize = 10)
		{
			List<MenuItems> existingMenuItems;
			try
			{
				existingMenuItems = await Services.GetAllMenuItemsPagedAsync(restaurantId, menuCategoryId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuItems);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddMenuItemAsync([FromBody] ItemModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuItems newMenuItem;
			try
			{
				People currentUser = GetCurrentUser();
				newMenuItem = await Services.AddMenuItemAsync(currentUser.ThePersonAsEmployer.Id,
						model.RestaurantId, model.MenuCategoryId, model.GetItemNameDictionary(),
						model.GetItemDescriptionDictionary(), model.GetItemWarningsDictionary(),
						model.GetItemPriceDictionary());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newMenuItem);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMenuItemAsync(long id, [FromBody] ItemModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuItems existingMenuItem;
			try
			{
				People currentUser = GetCurrentUser();
				existingMenuItem = await Services.UpdateMenuItemAsync(currentUser.ThePersonAsEmployer.Id,
						model.RestaurantId, id, model.GetItemNameDictionary(),
						model.GetItemDescriptionDictionary(), model.GetItemWarningsDictionary(),
						model.GetItemPriceDictionary());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuItem);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMenuItem(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveMenuCategoryAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}

	}
}

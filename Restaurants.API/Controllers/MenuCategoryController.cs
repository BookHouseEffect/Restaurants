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
    [Route("api/restaurant/menuCategories")]
    public class MenuCategoryController : BaseController
    {

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleMenuCategoryAsync(long id)
		{
			MenuCategories existingMenuCategory;
			try
			{
				existingMenuCategory = await Services.GetMenuCategoryAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCategory);
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<IActionResult> GetMenuCategoriesAsync(long restaurantId, int pageNumber=1, int pageSize = 10)
		{
			List<MenuCategories> existingMenuCategories;
			try
			{
				existingMenuCategories = await Services.GetAllMenuCategoriesAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCategories);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddMenuCategoryAsync([FromBody] CategoryModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuCategories newMenuCategory;
			try
			{
				People currentUser = GetCurrentUser();
				newMenuCategory = await Services.AddMenuCategoryAsync(currentUser.ThePersonAsEmployer.Id, 
						model.RestaurantId, model.GetNameDictionary(), model.GetDescriptionDictionary());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newMenuCategory);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMenuCateogryAsync(long id, [FromBody] CategoryModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			MenuCategories existingMenuCategory;
			try
			{
				People currentUser = GetCurrentUser();
				existingMenuCategory = await Services.UpdateMenuCategoryAsync(currentUser.ThePersonAsEmployer.Id, 
						model.RestaurantId, id, model.GetNameDictionary(), model.GetDescriptionDictionary());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingMenuCategory);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMenuCategory(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveMenuCategory(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}

	}
}
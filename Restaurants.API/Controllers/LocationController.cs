using Restaurants.API.Models.Api;
using Restaurants.API.Models.EntityFramework;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
    [Route("api/restaurant/location")]
    public class LocationController : BaseController
    {
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleAsync(long id)
		{
			LocationContact existingContact;
			try
			{
				existingContact = await Services.GetLocationContactAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetByRestaurantIdAsync(long restaurantId)
		{
			LocationContact existingContact;
			try
			{
				existingContact = await Services.GetContactAddressByRestaurantIdAsync(restaurantId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[HttpPost]
		public async Task<IActionResult> AddContactAddressAsync([FromBody]LocationModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			LocationContact newContact;
			try
			{
				People currentUser = GetCurrentUser();
				newContact = await Services.AddContactAddressAsync(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, model.Floor, model.StreetNumber, model.Route, model.Locality,
					model.Country, model.ZipCode, model.TheLocationPoint.Latitude, model.TheLocationPoint.Longitude,
					model.AdministrativeAreaLevel1, model.AdministrativeAreaLevel2, model.GoogleLink);

			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newContact);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateContactAddressAsync(long id, [FromBody] LocationModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			LocationContact updatedContact;
			try
			{
				People currentUser = GetCurrentUser();
				updatedContact = await Services.UpdateContactAddressAsync(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, id, model.Floor, model.StreetNumber, model.Route, model.Locality,
					model.Country, model.ZipCode, model.TheLocationPoint.Latitude, model.TheLocationPoint.Longitude,
					model.AdministrativeAreaLevel1, model.AdministrativeAreaLevel2, model.GoogleLink);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(updatedContact);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteContactAddress(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveContactAddressAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}
	}
}
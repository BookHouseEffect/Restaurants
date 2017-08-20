using Restaurants.API.Models.Api;
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
    [Route("api/restaurant/location")]
    public class LocationController : BaseController
    {
		[AllowAnonymous]
		[HttpGet("{id}")]
		public IActionResult GetSingle(long id)
		{
			LocationContact existingContact;
			try
			{
				existingContact = Services.GetLocationContact(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult GetAllContacts(long restaurantId, int pageNumber = 1, int pageSize = 10)
		{
			List<LocationContact> existingContact;
			try
			{
				existingContact = Services.GetAllContactAddresses(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[HttpPost]
		public IActionResult AddContactNumber(LocationModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			LocationContact newContact;
			try
			{
				People currentUser = GetCurrentUser();
				newContact = Services.AddContactAddress(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, model.Floor, model.StreetNumber, model.Route, model.Locality,
					model.Country, model.ZipCode, model.Latitude, model.Longitude, 
					model.AdministrativeAreaLevel1, model.AdministrativeAreaLevel2, model.GoogleLink);

			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newContact);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateContactNumber(long id, [FromBody] LocationModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			LocationContact updatedContact;
			try
			{
				People currentUser = GetCurrentUser();
				updatedContact = Services.UpdateContactAddress(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, id, model.Floor, model.StreetNumber, model.Route, model.Locality,
					model.Country, model.ZipCode, model.Latitude, model.Longitude,
					model.AdministrativeAreaLevel1, model.AdministrativeAreaLevel2, model.GoogleLink);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(updatedContact);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteContactNumber(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveContactAddress(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}
	}
}
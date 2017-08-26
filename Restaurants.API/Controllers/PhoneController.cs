using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Restaurants.API.Models.Api;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
	[Route("api/restaurant/phone")]
	public class PhoneController : BaseController
	{
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSingleAsync(long id)
		{
			PhoneContacts existingContact;
			try
			{
				existingContact = await Services.GetPhoneContactAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetAllContactsAsync(long restaurantId, int pageNumber = 1, int pageSize = 10)
		{
			List<PhoneContacts> existingContact;
			try
			{
				existingContact = await Services.GetAllContactNumbersAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingContact);
		}

		[HttpPost]
		public async Task<IActionResult> AddContactNumberAsync([FromBody] PhoneModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			PhoneContacts newContact;
			try
			{
				People currentUser = GetCurrentUser();
				newContact = await Services.AddContactNumberAsync(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, model.PhoneNumber, model.PhoneDescription);

			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newContact);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateContactNumberAsync(long id, [FromBody] PhoneModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			PhoneContacts updatedContact;
			try
			{
				People currentUser = GetCurrentUser();
				updatedContact = await Services.UpdateContactNumberAsync(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, id, model.PhoneNumber, model.PhoneDescription);
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
				Services.RemoveContactNumberAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}
	}
}
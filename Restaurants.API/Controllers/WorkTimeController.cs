using Restaurants.API.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
	[Route("api/restaurant/schedule")]
	public class WorkTimeController : BaseController
	{

		[AllowAnonymous]
		[HttpGet("{id}")]
		public IActionResult GetSchedules(long id)
		{
			OpenHoursSchedule existingSchedule;
			try
			{
				existingSchedule = Services.GetWorkTimeById(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingSchedule);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult GetAllSchedules(long restaurantId)
		{
			List<OpenHoursSchedule> existingSchedule;
			try
			{
				existingSchedule = Services.GetAllWorkingIntervals(restaurantId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingSchedule);
		}

		[HttpPost]
		public IActionResult AddSchedule([FromBody] ExtendedDayTimeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			OpenHoursSchedule newSchedule;
			try
			{
				People currentUser = GetCurrentUser();
				newSchedule = Services.AddWorkingInterval(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, model.GetUtcStartDay(), model.GetUtcStartTimeSpan(), 
					model.GetUtcEndtDay(), model.GetUtcEndTimeSpan());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newSchedule);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateSchedule(long id, [FromBody] ExtendedDayTimeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			OpenHoursSchedule existingSchedule;
			try
			{
				People currentUser = GetCurrentUser();
				existingSchedule = Services.UpdateWokingInterval(currentUser.ThePersonAsEmployer.Id,
					model.RestaurantId, id, model.GetUtcStartDay(), model.GetUtcStartTimeSpan(),
					model.GetUtcEndtDay(), model.GetUtcEndTimeSpan());
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingSchedule);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteSchedule(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.RemoveWorkingInterval(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}
	}
}
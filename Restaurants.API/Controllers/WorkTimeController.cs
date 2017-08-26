using Restaurants.API.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
	[Route("api/restaurant/schedule")]
	public class WorkTimeController : BaseController
	{

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSchedulesAsync(long id)
		{
			OpenHoursSchedule existingSchedule;
			try
			{
				existingSchedule = await Services.GetWorkTimeByIdAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingSchedule);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetAllSchedulesAsync(long restaurantId)
		{
			List<OpenHoursSchedule> existingSchedule;
			try
			{
				existingSchedule = await Services.GetAllWorkingIntervalsAsync(restaurantId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingSchedule);
		}

		[HttpPost]
		public async Task<IActionResult> AddScheduleAsync([FromBody] ExtendedDayTimeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			OpenHoursSchedule newSchedule;
			try
			{
				People currentUser = GetCurrentUser();
				newSchedule = await Services.AddWorkingIntervalAsync(currentUser.ThePersonAsEmployer.Id,
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
		public async Task<IActionResult> UpdateScheduleAsync(long id, [FromBody] ExtendedDayTimeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			OpenHoursSchedule existingSchedule;
			try
			{
				People currentUser = GetCurrentUser();
				existingSchedule = await Services.UpdateWokingIntervalAsync(currentUser.ThePersonAsEmployer.Id,
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
				Services.RemoveWorkingIntervalAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}
	}
}
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	interface IScheduleService
    {
		Task<OpenHoursSchedule> AddWorkingIntervalAsync(
			long employerId,
			long restaurantId,
			DayOfWeek startDay,
			TimeSpan startTime,
			DayOfWeek endDay,
			TimeSpan endTime
		);

		Task<OpenHoursSchedule> UpdateWokingIntervalAsync(
			long employerId,
			long restaurantId,
			long scheduleId,
			DayOfWeek startDay,
			TimeSpan startTime,
			DayOfWeek endDay,
			TimeSpan endTime
		);

		Task<OpenHoursSchedule> GetWorkTimeByIdAsync(
			long id
		);

		Task<List<OpenHoursSchedule>> GetAllWorkingIntervalsAsync(
			long restaurantId
		);

		Task<bool> RemoveWorkingIntervalAsync(
			long employerId,
			long restaurantId,
			long scheduleId
		);

		Task<OutOfSchedulePeriods> AddOutOfScheduleIntervalAsync(
			long employerId,
			long restaurantId,
			long openScheduleId,
			DateTimeOffset startOn,
			DateTimeOffset endsOn,
			string description
		);

		Task<List<OutOfSchedulePeriods>> GetAllOutOfScheduleIntervalsAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<OutOfSchedulePeriods> UpdateOutOfScheduleIntervalsAsync(
			long employerId,
			long restaurantId,
			long scheduleId,
			DateTimeOffset startOn,
			DateTimeOffset endsOn,
			string description
		);

		Task<bool> RemoveOutOfScheduleIntervalAsync(
		   long employerId,
			long restaurantId,
			long scheduleId
		);
	}
}

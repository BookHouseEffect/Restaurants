using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;

namespace Restaurants.API.Services.Helpers
{
	interface IScheduleService
    {
		OpenHoursSchedule AddWorkingInterval(
			long employerId,
			long restaurantId,
			DayOfWeek startDay,
			TimeSpan startTime,
			DayOfWeek endDay,
			TimeSpan endTime
		);

		OpenHoursSchedule UpdateWokingInterval(
			long employerId,
			long restaurantId,
			long scheduleId,
			DayOfWeek startDay,
			TimeSpan startTime,
			DayOfWeek endDay,
			TimeSpan endTime
		);

		OpenHoursSchedule GetWorkTimeById(
			long id
		);

		List<OpenHoursSchedule> GetAllWorkingIntervals(
			long restaurantId
		);

		bool RemoveWorkingInterval(
			long employerId,
			long restaurantId,
			long scheduleId
		);

		OutOfSchedulePeriods AddOutOfScheduleInterval(
			long employerId,
			long restaurantId,
			long openScheduleId,
			DateTimeOffset startOn,
			DateTimeOffset endsOn,
			string description
		);

		List<OutOfSchedulePeriods> GetAllOutOfScheduleIntervals(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		OutOfSchedulePeriods UpdateOutOfScheduleIntervals(
			long employerId,
			long restaurantId,
			long scheduleId,
			DateTimeOffset startOn,
			DateTimeOffset endsOn,
			string description
		);

		bool RemoveOutOfScheduleInterval(
		   long employerId,
			long restaurantId,
			long scheduleId
		);
	}
}

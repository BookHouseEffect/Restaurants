using System.Linq;
using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Implementation
{
	public class ScheduleService : BaseService, IScheduleService
	{
		private const int MinutesInOneDay = 1440;
		private const int MinutesInOneWeek = 10080;

		private ScheduleRepository ScheduleRepo;
		private OutOfScheduleRepository OutOfScheduleRepo;

		public ScheduleService(AppDbContext dbContext, People logedInPerson) 
			: base(dbContext, logedInPerson)
		{
			this.ScheduleRepo = new ScheduleRepository(dbContext);
			this.OutOfScheduleRepo = new OutOfScheduleRepository(dbContext);
		}

		public async Task<OutOfSchedulePeriods> AddOutOfScheduleIntervalAsync(long employerId, long restaurantId, long openScheduleId, DateTimeOffset startOn, DateTimeOffset endsOn, string description)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(employerId, restaurantId);
			OpenHoursSchedule work = await CheckScheduleExistanceAsync(openScheduleId);
			if (restaurantId != work.RestaurantId)
				throw new Exception("Operation not permitted. Cannot add schedule for other restaurants.");

			CheckTheLoggedInPerson();

			if (startOn.CompareTo(endsOn) >= 0)
				throw new Exception(String.Format("Invalid period range. The period range can not start on: {0} and ends on {1}.", startOn.ToString("s"), endsOn.ToString("S")));

			DateTimeOffset current = DateTimeOffset.UtcNow;
			if (endsOn.CompareTo(current) <= 0)
				throw new Exception(String.Format("The interval ({0} - {1}) is before current time {2}, and can not be added.", startOn.ToString("s"), endsOn.ToString("s"), current.ToString("s")));

			DateTimeOffset real = startOn.CompareTo(current) <= 0 ? current : startOn;

			OutOfSchedulePeriods outSchedule = new OutOfSchedulePeriods
			{
				OutOfSchedulePeriodStarts = real,
				OutOfSchedulePeriodEnds = endsOn,
				Description = description,
				OpenHoursScheduleId = openScheduleId
			};

			List<OutOfSchedulePeriods> existingSchedules = await OutOfScheduleRepo.GetAllInPeriod(openScheduleId, real, endsOn);

			if (existingSchedules.Count == 0)
			{
				OutOfScheduleRepo.Add(outSchedule, this.ModifierId);
				return outSchedule;
			}

			throw new Exception("There are overlapping periods");
		}

		public async Task<OpenHoursSchedule> AddWorkingIntervalAsync(long employerId, long restaurantId, DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(employerId, restaurantId);
			CheckTheLoggedInPerson();

			OpenHoursSchedule schedule = new OpenHoursSchedule
			{
				StartDay = startDay,
				StartTime = startTime,
				EndDay = endDay,
				EndTime = endTime,
				RestaurantId = restaurantId,
			};

			List<OpenHoursSchedule> currentSchedules = await ScheduleRepo.FindByRestaurantId(restaurantId);

			List<OpenHoursSchedule> overlappingShedules = GetOverlappedSchedule(schedule, currentSchedules);

			if (overlappingShedules.Count == 0)
			{
				ScheduleRepo.Add(schedule, this.ModifierId);
				return schedule;
			}

			OpenHoursSchedule newSchedule = SolveOverlappedSchedules(schedule, overlappingShedules);

			newSchedule.RestaurantId = restaurantId;
			ScheduleRepo.Add(newSchedule, this.ModifierId);

			foreach (var sch in overlappingShedules)
				ScheduleRepo.Remove(sch);

			return newSchedule;
		}

		public async Task<OpenHoursSchedule> GetWorkTimeByIdAsync(long id)
		{
			return await CheckScheduleExistanceAsync(id);
		}

		public async Task<List<OutOfSchedulePeriods>> GetAllOutOfScheduleIntervalsAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return await OutOfScheduleRepo.GetAllInPeriod(restaurantId, pageNumber, pageSize);
		}

		public async Task<List<OpenHoursSchedule>> GetAllWorkingIntervalsAsync(long restaurantId)
		{
			return await ScheduleRepo.FindByRestaurantId(restaurantId);
		}

		public async Task<bool> RemoveOutOfScheduleIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(employerId, restaurantId);
			CheckTheLoggedInPerson();

			OutOfSchedulePeriods current = await CheckOutPeriodExistanceAsync(scheduleId);

			OutOfScheduleRepo.Remove(current);
			return true;
		}

		public async Task<bool> RemoveWorkingIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(employerId, restaurantId);
			CheckTheLoggedInPerson();

			OpenHoursSchedule current = await CheckScheduleExistanceAsync(scheduleId);

			ScheduleRepo.Remove(current);
			return true;
		}

		public Task<OutOfSchedulePeriods> UpdateOutOfScheduleIntervalsAsync(long employerId, long restaurantId, long scheduleId, DateTimeOffset startOn, DateTimeOffset endsOn, string description)
		{
			//TODO: UpdateOutOfScheduleIntervals
			throw new NotImplementedException();
		}

		public async Task<OpenHoursSchedule> UpdateWokingIntervalAsync(long employerId, long restaurantId, long scheduleId, DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(employerId, restaurantId);
			CheckTheLoggedInPerson();

			List<OpenHoursSchedule> currentSchedules = await ScheduleRepo.FindByRestaurantId(restaurantId);
			OpenHoursSchedule existing = currentSchedules.Where(x => x.Id == scheduleId).Single();

			existing.StartDay = startDay;
			existing.StartTime = startTime;
			existing.EndDay = endDay;
			existing.EndTime = endTime;

			List<OpenHoursSchedule> overlappingSchedules = GetOverlappedSchedule(existing, currentSchedules);

			if (overlappingSchedules.Count == 0)
			{
				ScheduleRepo.Update(existing, this.ModifierId);
				return existing;
			}

			OpenHoursSchedule newSchedule = SolveOverlappedSchedules(existing, overlappingSchedules);

			existing.StartDay = newSchedule.StartDay;
			existing.StartTime = newSchedule.StartTime;
			existing.EndDay = newSchedule.EndDay;
			existing.EndTime = newSchedule.EndTime;
			ScheduleRepo.Update(existing, this.ModifierId);

			foreach (var sch in overlappingSchedules)
				ScheduleRepo.Remove(sch);

			return existing;
		}

		private int GetMinRange(OpenHoursSchedule schedule)
		{
			return (int)schedule.StartDay * MinutesInOneDay + (int)schedule.StartTime.TotalMinutes;
		}

		private int GetMaxRange(OpenHoursSchedule schedule)
		{
			return (int)schedule.EndDay * MinutesInOneDay + (int)schedule.EndTime.TotalMinutes;
		}

		private List<OpenHoursSchedule> GetOverlappedSchedule(OpenHoursSchedule currentSchedule, List<OpenHoursSchedule> schedules)
		{
			int minRange = GetMinRange(currentSchedule);
			int maxRange = GetMaxRange(currentSchedule);

			if (maxRange < minRange)
				maxRange += MinutesInOneWeek;

			List<OpenHoursSchedule> overlapped = new List<OpenHoursSchedule>();
			foreach (var sch in schedules)
			{
				if (currentSchedule.Id == sch.Id)
					continue;

				int schMin = GetMinRange(sch);
				int schMax = GetMaxRange(sch);

				if (schMax < schMin)
					schMax += MinutesInOneWeek;

				bool predicate =
					   // goes out of the boundaries in boths way
					   minRange <= schMin && maxRange >= schMax

					   // goes inside the boundaries both ways
					|| minRange >= schMin && maxRange <= schMax

					   // left side goes inside
					|| minRange >= schMin && minRange <= schMax

					   // right side goes inside
					|| maxRange >= schMin && maxRange <= schMax;    

				if (predicate) overlapped.Add(sch);
			}

			return overlapped;
		}

		private OpenHoursSchedule SolveOverlappedSchedules(OpenHoursSchedule currentSchedule, List<OpenHoursSchedule> schedules)
		{
			int minRange = GetMinRange(currentSchedule);
			int maxRange = GetMaxRange(currentSchedule);

			if (maxRange < minRange)
				maxRange += MinutesInOneWeek;

			foreach (var sch in schedules)
			{
				int schMin = GetMinRange(sch);
				int schMax = GetMaxRange(sch);

				if (schMax < schMin) schMax += MinutesInOneWeek;
				if (schMin < minRange) minRange = schMin;
				if (schMax > maxRange) maxRange = schMax;
			}

			maxRange %= MinutesInOneWeek;

			OpenHoursSchedule solvedSchedule = new OpenHoursSchedule
			{
				StartDay = (DayOfWeek)(minRange / MinutesInOneDay),
				StartTime = TimeSpan.FromMinutes(minRange % MinutesInOneDay),
				EndDay = (DayOfWeek)(maxRange / MinutesInOneDay),
				EndTime = TimeSpan.FromMinutes(maxRange % MinutesInOneDay)
			};

			return solvedSchedule;
		}

		private async Task<OpenHoursSchedule> CheckScheduleExistanceAsync(long scheduleId)
		{
			OpenHoursSchedule schedule = await ScheduleRepo.FindById(scheduleId);
			if (schedule == null)
				throw new Exception(String.Format("There is no schedule record with id {0}", scheduleId));
			return schedule;
		}

		private async Task<OutOfSchedulePeriods> CheckOutPeriodExistanceAsync(long scheduleId)
		{
			OutOfSchedulePeriods schedule = await OutOfScheduleRepo.FindById(scheduleId);
			if (schedule == null)
				throw new Exception(String.Format("There is no schedule record with id {0}", scheduleId));
			return schedule;
		}


	}
}

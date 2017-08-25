using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System;
using System.Collections.Generic;

namespace Restaurants.API.Persistence.Implementation
{
	public class OutOfScheduleRepository : CrudRepository<OutOfSchedulePeriods>
	{
		public OutOfScheduleRepository(AppDbContext dbContext) : base(dbContext) { }

		internal List<OutOfSchedulePeriods> GetAllInPeriod(long openScheduleId, DateTimeOffset periodStartOn, DateTimeOffset periodEndOn)
		{
			return DbSet
				.Where(
					x => x.OpenHoursScheduleId == openScheduleId &&
					(
						(
							x.OutOfSchedulePeriodStarts > periodStartOn
							&& x.OutOfSchedulePeriodStarts < periodEndOn
						)
						||
						(
							x.OutOfSchedulePeriodEnds > periodStartOn
							&& x.OutOfSchedulePeriodEnds < periodEndOn
						)
					)
				).ToList();
		}

		internal List<OutOfSchedulePeriods> GetAllInPeriod(long restaurantId, int pageNumber, int pageSize)
		{
			return DbSet
				.Where(x => x.TheRealSchedule.RestaurantId == restaurantId)
				.Include(x=>x.CreatedBy)
				.Include(x=>x.ModifiedBy)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}
}

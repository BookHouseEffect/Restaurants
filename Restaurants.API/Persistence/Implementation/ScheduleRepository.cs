using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class ScheduleRepository : CrudRepository<OpenHoursSchedule>
	{
		public ScheduleRepository(AppDbContext dbContext) : base(dbContext) { }

		internal Task<List<OpenHoursSchedule>> FindByRestaurantId(long restaurantId)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.Include(x=>x.CreatedBy)
				.Include(x=>x.ModifiedBy)
				.ToListAsync();
		}
	}
}

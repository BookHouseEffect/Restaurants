using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class EmployersRestaurantsRepository : CrudRepository<EmployersRestaurants>
	{
		public EmployersRestaurantsRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<EmployersRestaurants> GetByRestaurantIdAndEmployerId(long restaurantId, long employerId)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId && x.EmployerId == employerId)
				.Include(x => x.TheRestaurant)
				.Include(x => x.TheEmployer)
				.SingleOrDefaultAsync();
		}

		public Task<List<EmployersRestaurants>> GetByRestaurantId(long restaurantId)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.ToListAsync();
		}

		public Task<List<EmployersRestaurants>> GetOwnersByRestaurantIdPaged(long restaurantId, int pageNumber, int pageSize)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheEmployer)
				.ThenInclude(x => x.TheEmployerDetails)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public Task<long> GetNumbetOfEmployers(long restaurantId)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.LongCountAsync();
		}
	}

}

using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.API.Persistence.Implementation
{
	public class RestaurantRepository : CrudRepository<RestaurantObjects>
	{
		public RestaurantRepository(AppDbContext dbContext) : base(dbContext) { }

		public List<RestaurantObjects> GetRestaurantsByOwnerIdPaged(long ownerId, int pageNumber, int pageSize)
		{
			return this.DbSet
			.Where(x => x.TheRestaurantEmployers.Any(y => y.EmployerId == ownerId))
			.Include(x => x.CreatedBy)
			.Include(x => x.ModifiedBy)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToList();
		}

	}
}

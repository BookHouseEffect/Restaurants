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

		public List<Employers> GetOwnersByRestaurantIdPaged(long restaurantId, int pageNumber, int pageSize)
		{
			return _dbContext
				.RestaurantObjects
				.Where(x => x.Id == restaurantId)
				.SelectMany(x => x.TheRestaurantEmployers)
				.Select(x => x.TheEmployer)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}

	public class EmployersRestaurantsRepository : CrudRepository<EmployersRestaurants>
	{
		public EmployersRestaurantsRepository(AppDbContext dbContext) : base(dbContext) { }

		public List<EmployersRestaurants> GetByRestaurantId(long restaurantId)
		{
			return _dbContext
				.EmployersRestaurants
				.Where(x => x.RestaurantId == restaurantId)
				.ToList();
		}

		public EmployersRestaurants GetByRestaurantIdAndEmployerId(long restaurantId, long employerId)
		{
			return _dbContext
				.EmployersRestaurants
				.Where(x => x.RestaurantId == restaurantId && x.EmployerId == employerId)
				.Include(x => x.TheRestaurant)
				.Include(x => x.TheEmployer)
				.SingleOrDefault();
		}
	}

	public class EmployersRepository : CrudRepository<Employers>
	{
		public EmployersRepository(AppDbContext dbContext) : base(dbContext) { }

		public List<RestaurantObjects> GetRestaurantsByOwnerIdPaged(long ownerId, int pageNumber, int pageSize)
		{
			return _dbContext
			.Employers
			.Where(x => x.Id == ownerId)
			.SelectMany(x => x.TheEmployerRestaurantsOwned)
			.Select(x => x.TheRestaurant)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToList();
		}
	}
}

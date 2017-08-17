using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Restaurants.API.Persistence.Implementation
{
	public class EmployersRepository : CrudRepository<Employers>
	{
		public EmployersRepository(AppDbContext dbContext) : base(dbContext) { }

		public List<Employers> GetEmployersNotOwnersToCurrentRestaurant(string filter, long restaurantId, int pageNumber, int pageSize)
		{
			string[] filterWords = filter.Split(' ');
			return _dbContext
				.Employers
				.Where(
					x => !(x.TheEmployerRestaurantsOwned.Any(y => y.RestaurantId == restaurantId))
						&& filterWords.All(
							y => x.TheEmployerDetails.PersonFirstName.ToLower().Contains(y.ToLower())
							|| x.TheEmployerDetails.PersonLastName.ToLower().Contains(y.ToLower())
							|| x.TheEmployerDetails.PersonMiddleName != null &&
								x.TheEmployerDetails.PersonMiddleName.ToLower().Contains(y.ToLower())
							)
				).Include(x => x.TheEmployerDetails)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}
}

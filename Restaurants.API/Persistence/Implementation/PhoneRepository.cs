using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;

namespace Restaurants.API.Persistence.Implementation
{
	public class PhoneRepository : CrudRepository<PhoneContacts>
	{
		public PhoneRepository(AppDbContext dbContext) : base(dbContext) { }

		internal List<PhoneContacts> GetPhoneNumbersByRestaurantPaged(long restaurantId, int pageNumber, int pageSize)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}
}

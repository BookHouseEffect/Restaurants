using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class LocationRepository : CrudRepository<LocationContact>
	{
		public LocationRepository(AppDbContext dbContext) : base(dbContext) { }

		internal Task<LocationContact> GetLocationsByRestaurantId(long restaurantId)
		{
			return this.DbSet
				.Where(x => x.RestaurantId == restaurantId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheLocationPoint)
				.SingleOrDefaultAsync();
		}
	}
}

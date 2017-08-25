using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;

namespace Restaurants.API.Persistence.Implementation
{
	public class LocationPointRepository : CrudRepository<LocationPoints>
	{
		public LocationPointRepository(AppDbContext dbContext) : base(dbContext) { }
	}
}

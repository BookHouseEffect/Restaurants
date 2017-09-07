using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Persistence.Implementation
{
	public class EmployeesRepository : CrudRepository<Employees>
	{
		public EmployeesRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<List<Employees>> GetEmployeesByRestaurantId(long restaurantId, int pageNumber, int pageSize)
		{
			return this.DbSet
				.Where(x => x.RestaurantId.HasValue && x.RestaurantId == restaurantId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheEmployeeDetails)
					.ThenInclude(x => x.ModifiedBy)
				.Include(x => x.TheAssignedTypes)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}

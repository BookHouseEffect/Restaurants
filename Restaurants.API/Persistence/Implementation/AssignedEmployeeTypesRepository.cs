using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.API.Persistence.Implementation
{
	public class AssignedEmployeeTypesRepository : CrudRepository<AssignedEmployeeTypes>
	{
		public AssignedEmployeeTypesRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<List<AssignedEmployeeTypes>> GetTypesForEmployee(long employeeId)
		{
			return this.DbSet
				.Where(x => x.EmployeeId == employeeId)
				.ToListAsync();
		}
	}
}

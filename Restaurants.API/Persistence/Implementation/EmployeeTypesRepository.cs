using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class EmployeeTypesRepository : BaseRepository
	{
		public EmployeeTypesRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<EmployeeTypes> GetEmployeeTypeById(long employeeTypeId)
		{
			return _dbContext
				.EmployeeType
				.Where(x => x.Id == employeeTypeId)
				.SingleAsync();
		}

		public Task<List<EmployeeTypes>> GetEmployeeTypeList()
		{
			return _dbContext
				.EmployeeType
				.ToListAsync();
		}
	}
}

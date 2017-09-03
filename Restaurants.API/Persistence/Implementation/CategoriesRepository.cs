using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class CategoriesRepository : CrudRepository<Categories>
	{
		public CategoriesRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<List<Categories>> GetByMenuCategoryId(long menuCategoryId)
		{
			return DbSet
				.Where(x => x.MenuCategoryId == menuCategoryId)
				.ToListAsync();
		}

		internal Task<List<Categories>> GetByMenuLanguageId(long menuLanguageId)
		{
			return DbSet
				.Where(x => x.MenuLanguageId == menuLanguageId)
				.ToListAsync();
		}
	}

}

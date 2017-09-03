using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;

namespace Restaurants.API.Persistence.Implementation
{
	public class MenuCategoriesRepository : CrudRepository<MenuCategories>
	{
		public MenuCategoriesRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<MenuCategories> GetSingleMenuCategoryById(long menuCategoriesId)
		{
			return DbSet
				.Where(x => x.Id == menuCategoriesId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.TheMenuLanguage)
						.ThenInclude(u => u.TheLanguage)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.CreatedBy)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.ModifiedBy)
				.SingleOrDefaultAsync();
		}

		internal Task<List<MenuCategories>> GetMenuCategoriesByMenuIdPaged(long menuId, int pageNumber, int pageSize)
		{
			return DbSet
				.Where(x => x.TheCategories.All(y => y.TheMenuLanguage.MenuId == menuId))
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.TheMenuLanguage)
						.ThenInclude(u => u.TheLanguage)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.CreatedBy)
				.Include(x => x.TheCategories)
					.ThenInclude(t => t.ModifiedBy)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}

}

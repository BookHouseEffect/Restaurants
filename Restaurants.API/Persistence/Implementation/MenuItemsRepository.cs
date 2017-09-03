using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System;

namespace Restaurants.API.Persistence.Implementation
{
	public class MenuItemsRepository : CrudRepository<MenuItems>
	{
		public MenuItemsRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<MenuItems> GetSingleById(long menuItemId)
		{
			return this.DbSet
				.Where(x => x.Id == menuItemId)
				.Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.TheMenuLanguage)
						.ThenInclude(x => x.TheLanguage)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.CreatedBy)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.ModifiedBy)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.TheMenuCurrency)
						.ThenInclude(x => x.TheCurrency)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.CreatedBy)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.ModifiedBy)
				.SingleOrDefaultAsync();
		}

		internal Task<List<MenuItems>> GetItemsByMenuIdAndMenuCategoryIdPaged(long menuId, long menuCategoryId, int pageNumber, int pageSize)
		{
			return this.DbSet
				.Where(
					x => x.MenuId == menuId
					&& x.MenuCategoryId == menuCategoryId
				).Include(x => x.CreatedBy)
				.Include(x => x.ModifiedBy)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.TheMenuLanguage)
						.ThenInclude(x => x.TheLanguage)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.CreatedBy)
				.Include(x => x.TheContent)
					.ThenInclude(x => x.ModifiedBy)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.TheMenuCurrency)
						.ThenInclude(x => x.TheCurrency)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.CreatedBy)
				.Include(x => x.TheValue)
					.ThenInclude(x => x.ModifiedBy)
				.ToListAsync();
		}

		internal Task<List<MenuItems>> GetItemByCategoryId(long menuCategoryId)
		{
			return this.DbSet
				.Where(x => x.MenuCategoryId == menuCategoryId)
				.ToListAsync();
		}
	}
}

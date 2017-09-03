using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace Restaurants.API.Persistence.Implementation
{
	public class MenuItemValueRepository : CrudRepository<MenuItemValues>
	{
		public MenuItemValueRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<List<MenuItemValues>> GetByMenuItemId(long menuItemId)
		{
			return this.DbSet
				.Where(x => x.MenuItemId == menuItemId)
				.ToListAsync();
		}

		internal Task<List<MenuItemValues>> GetByMenuCurrencyId(long menuCurrencyId)
		{
			return this.DbSet
				.Where(x => x.MenuCurrencyId == menuCurrencyId)
				.ToListAsync();
		}
	}
}

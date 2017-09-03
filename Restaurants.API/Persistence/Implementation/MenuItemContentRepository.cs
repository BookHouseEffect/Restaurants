using Microsoft.EntityFrameworkCore;
using System.Linq;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class MenuItemContentRepository : CrudRepository<MenuItemContents>
	{
		public MenuItemContentRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<List<MenuItemContents>> GetByMenuItemId(long menuItemId)
		{
			return this.DbSet
				.Where(x => x.MenuItemId == menuItemId)
				.ToListAsync();
		}

		internal Task<List<MenuItemContents>> GetByMenuLanguageId(long menuLanguageId)
		{
			return this.DbSet
				.Where(x => x.MenuLanguageId == menuLanguageId)
				.ToListAsync();
		}
	}
}

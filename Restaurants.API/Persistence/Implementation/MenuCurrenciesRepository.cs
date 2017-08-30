using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{ 
	public class MenuCurrenciesRepository : CrudRepository<MenuCurrencies>
	{
		public MenuCurrenciesRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		internal Task<List<MenuCurrencies>> GetItemsByMenuId(long menuId)
		{
			return this.DbSet
				.Where(x => x.MenuId == menuId)
				.Include(x => x.ModifiedBy)
				.Include(x => x.CreatedBy)
				.Include(x => x.TheCurrency)
				.ToListAsync();
		}

		internal Task<List<MenuCurrencies>> GetItemsByMenuIdPaged(long menuId, int pageNumber, int pageSize)
		{
			return this.DbSet
				.Where(x => x.MenuId == menuId)
				.Include(x => x.ModifiedBy)
				.Include(x => x.CreatedBy)
				.Include(x => x.TheCurrency)
				.Skip((pageNumber - 1 )*pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}

}

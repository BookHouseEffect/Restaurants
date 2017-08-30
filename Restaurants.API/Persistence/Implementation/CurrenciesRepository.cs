using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class CurrenciesRepository : BaseRepository
	{
		public CurrenciesRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<Currencies> GetCurrencyById(long currencyId)
		{
			return _dbContext
				.Currencies
				.Where(x => x.Id == currencyId)
				.SingleAsync();
		}

		public Task<List<Currencies>> GetCurrencyList()
		{
			return _dbContext
				.Currencies
				.ToListAsync();
		}
	}
}

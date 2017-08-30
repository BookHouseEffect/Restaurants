using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Persistence.Implementation
{
	public class LanguagesRepository : BaseRepository
	{
		public LanguagesRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<Languages> GetLanguageById(long languageId)
		{
			return _dbContext
				.Languages
				.Where(x => x.Id == languageId)
				.SingleAsync();
		}

		public Task<List<Languages>> GetLanguaseList()
		{
			return _dbContext
				.Languages
				.ToListAsync();
		}
	}
}

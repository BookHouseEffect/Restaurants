using Restaurants.API.Services.Helpers;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Implementation
{
	public class SearchService : BaseService, ISearchService
	{

		public SearchService(AppDbContext dbContext) : base(dbContext, null) { }

		public async Task<List<Employers>> SearchEmployersAsync(string searchPhrase, long restaurantId, int pageNumber, int pageSize)
		{
			return await EmployersRepo.GetEmployersNotOwnersToCurrentRestaurant(searchPhrase, restaurantId, pageNumber, pageSize);
		}
	}
}

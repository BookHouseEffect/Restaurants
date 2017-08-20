using Restaurants.API.Services.Helpers;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Services.Implementation
{
	public class SearchService : BaseService, ISearchService
	{

		public SearchService(AppDbContext dbContext) : base(dbContext, null) { }

		public List<Employers> SearchEmployers(string searchPhrase, long restaurantId, int pageNumber, int pageSize)
		{
			return EmployersRepo.GetEmployersNotOwnersToCurrentRestaurant(searchPhrase, restaurantId, pageNumber, pageSize);
		}
	}
}
